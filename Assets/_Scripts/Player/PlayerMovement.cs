using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 8f;
    [SerializeField] private float runSpeed = 16f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 14f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private LayerMask groundLayer;

    private Vector2 moveInput;
    private bool isSprinting;
    private bool isGrounded;
    private bool isJumping;
    private bool isMoving;

    #region [Components]
    private Rigidbody2D rb;
    private Animator anim;
    #endregion

    #region [Unity Lifecycle]
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.freezeRotation = true;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    private void Update()
    {
        HandleInput();
        HandleAnimation();
        HandleFlip();
    }

    private void FixedUpdate()
    {
        //CheckGround(); sementara gk bisa lompat nanti kode lompatnya ku hapus kalau emang gak ada lompat
        HandleMoving();
    }
    #endregion

    #region [Input]
    private void HandleInput()
    {
        moveInput = InputManager.Instance.GetPlayerMovement();
        moveInput.y = 0f;

        if (moveInput.magnitude > 1f) moveInput.Normalize();

        //if (InputManager.Instance.GetPlayerJumpPressed())
        //{
        //    Jump();
        //}
    }
    #endregion

    #region [Movement]
    private void HandleMoving()
    {
        isSprinting = InputManager.Instance.GetPlayerSprinting();
        float speed = isSprinting ? runSpeed : walkSpeed;
        rb.linearVelocity = new Vector2(moveInput.x * speed, rb.linearVelocity.y);
    }

    //public void Jump() ku matiin sementara ntar kalo mau dipake di uncomment aja
    //{
    //    if (!isGrounded) return;

    //    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    //    isGrounded = false;
    //    isJumping = true;
    //}
    #endregion

    #region [Ground]
    private void CheckGround()
    {
        if (groundCheck == null)
        {
            isGrounded = false;
            return;
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer) != null;

        // Land only when grounded and no longer moving up, so the jump isn't cancelled on the first frame
        if (isGrounded && rb.linearVelocity.y <= 0f)
        {
            isJumping = false;
        }
    }
    #endregion

    #region [Animation]
    private void HandleAnimation()
    {
        if (anim == null) return;

        isMoving = Mathf.Abs(moveInput.x) > 0.01f;

        anim.SetBool("isWalking", isMoving && !isSprinting);
        anim.SetBool("isRunning", isMoving && isSprinting);
        anim.SetBool("isJumping", isJumping);
    }
    #endregion

    #region [Flip]
    private void HandleFlip()
    {
        if (Mathf.Abs(moveInput.x) < 0.01f) return;

        Vector3 localScale = transform.localScale;
        localScale.x = moveInput.x > 0 ? Mathf.Abs(localScale.x) : -Mathf.Abs(localScale.x);
        transform.localScale = localScale;
    }
    #endregion

    #region [Gizmos]
    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
    #endregion
}