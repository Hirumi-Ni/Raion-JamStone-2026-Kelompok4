using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 8f;
    [SerializeField] private float runSpeed = 16f;

    [Header("Slope")]
    [SerializeField] private float maxSlopeAngle = 50f;
    [SerializeField] private float surfaceAnchorDistance = 0.1f;

    [Header("Ground")]
    [SerializeField] private LayerMask groundLayer;

    private Vector2 moveInput;
    private bool isMoving;
    private bool isSprinting;
    private bool isGrounded;

    #region [Components]
    private Rigidbody2D rb;
    private Animator anim;
    #endregion

    #region [Slide]
    private Rigidbody2D.SlideMovement slideMovement;
    private Rigidbody2D.SlideResults slideResults;
    #endregion

    #region [Unity Lifecycle]
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0f;
        rb.freezeRotation = true;

        SetupSlideMovement();
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

    #region [Setup]
    private void SetupSlideMovement()
    {
        slideMovement = new Rigidbody2D.SlideMovement
        {
            surfaceUp = Vector2.up,
            surfaceAnchor = Vector2.down * surfaceAnchorDistance,
            surfaceSlideAngle = maxSlopeAngle,
            maxIterations = 5,
            gravity = Vector2.zero,
            layerMask = groundLayer,
            useLayerMask = true,
            useAttachedTriggers = false,
            useSimulationMove = true
        };
    }
    #endregion

    #region [Input]
    private void HandleInput()
    {
        moveInput = InputManager.Instance.GetPlayerMovement();
        moveInput.y = 0f;

        if (moveInput.magnitude > 1f) moveInput.Normalize();
    }
    #endregion

    #region [Movement]
    private void HandleMoving()
    {
        isSprinting = InputManager.Instance.GetPlayerSprinting();
        float speed = isSprinting ? runSpeed : walkSpeed;

        Vector2 velocity = new Vector2(moveInput.x * speed, 0f);
        slideResults = rb.Slide(velocity, Time.fixedDeltaTime, slideMovement);

        UpdateGrounded();
    }
    #endregion

    #region [Ground]
    private void UpdateGrounded()
    {
        isGrounded = false;

        if (!slideResults.surfaceHit) return;

        Vector2 normal = slideResults.surfaceHit.normal;
        float angle = Vector2.Angle(normal, Vector2.up);
        if (angle <= maxSlopeAngle)
        {
            isGrounded = true;
        }
    }
    #endregion

    #region [Animation]
    private void HandleAnimation()
    {
        isMoving = Mathf.Abs(moveInput.x) > 0.01f;

        bool isWalking = isMoving && !isSprinting;
        bool isRunning = isMoving && isSprinting;

        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isRunning", isRunning);
    }
    #endregion

    #region [Flip]
    private void HandleFlip()
    {
        if (Mathf.Abs(moveInput.x) < 0.01f) return;
        Vector3 localScale = transform.localScale;

        if (moveInput.x > 0) localScale.x = Mathf.Abs(localScale.x);
        else localScale.x = -Mathf.Abs(localScale.x);
        transform.localScale = localScale;
    }
    #endregion
}
