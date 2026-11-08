using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 8f;
    [SerializeField] private float runSpeed = 16f;

    private Vector2 moveInput;
    private bool isSprinting;
    private bool isMoving;
    private bool lockMovement;

    #region [Components]
    private Rigidbody2D rb;
    private Animator anim;
    private PlayerInteraction interact;
    #endregion

    #region [Unity Lifecycle]
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        interact = GetComponent<PlayerInteraction>();
    }

    private void Update()
    {
        HandleInput();
        HandleAnimation();
        HandleFlip();
    }

    private void FixedUpdate()
    {
        HandleMoving();
    }
    #endregion

    #region [Input]
    private void HandleInput()
    {
        if (interact.PuzzleOpen || lockMovement)
        {
            moveInput = Vector2.zero;
            return;
        }

        moveInput = InputManager.Instance.GetPlayerMovement();
        moveInput.y = 0f;

        if (moveInput.magnitude > 1f) moveInput.Normalize();
    }
    #endregion

    #region [Movement]
    private void HandleMoving()
    {
        if (lockMovement) return;

        isSprinting = InputManager.Instance.GetPlayerSprinting();
        float speed = isSprinting ? runSpeed : walkSpeed;
        rb.linearVelocity = new Vector2(moveInput.x * speed, rb.linearVelocity.y);
        isMoving = Mathf.Abs(moveInput.x) > 0.01f;
    }

    public void LockMovement()
    {
        lockMovement = true;
    }
    #endregion

    #region [Animation]
    private void HandleAnimation()
    {
        if (anim == null) return;

        anim.SetBool("isWalking", isMoving && !isSprinting);
        anim.SetBool("isRunning", isMoving && isSprinting);
    }

    public void SetCutsceneMoving(bool moving)
    {
        isMoving = moving;
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
}