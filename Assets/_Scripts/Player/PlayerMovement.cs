using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 8f;
    [SerializeField] private float runSpeed = 16f;
    private Vector2 moveInput;
    private bool isMoving;
    private bool isSprinting;

    #region [Components]
    private Rigidbody2D rigidbody;
    private Animator anim;
    #endregion

    #region [Unity Lifecycle Method]
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {

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

    #region [Main Method]
    private void HandleInput()
    {
        moveInput = InputManager.Instance.GetPlayerMovement();
        moveInput.y = 0;
        if (moveInput.magnitude > 1f) moveInput.Normalize();
    }

    private void HandleMoving()
    {
        isSprinting = InputManager.Instance.GetPlayerSprinting();
        float speed = isSprinting ? runSpeed : walkSpeed;
        rigidbody.linearVelocity = new Vector2(moveInput.x * speed, rigidbody.linearVelocity.y);
    }

    private void HandleAnimation()
    {
        isMoving = Mathf.Abs(moveInput.x) > 0.01f;

        bool isWalking = isMoving && !isSprinting; 
        bool isRunning = isMoving && isSprinting; 

        anim.SetBool("isWalking", isWalking); 
        anim.SetBool("isRunning", isRunning);
    }
    #endregion

    #region [Helper Method]
    private void HandleFlip() 
    {
        if (Mathf.Abs(moveInput.x) < 0.01f) return; 
        
        Vector3 localScale = transform.localScale; 
        
        if (moveInput.x > 0) localScale.x = Mathf.Abs(localScale.x); 
        else if (moveInput.x < 0) localScale.x = -Mathf.Abs(localScale.x);
        
        transform.localScale = localScale;
    }
    #endregion
}
