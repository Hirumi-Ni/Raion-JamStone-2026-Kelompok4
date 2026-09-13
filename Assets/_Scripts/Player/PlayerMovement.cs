using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 8f;
    [SerializeField] private float runSpeed = 16f;
    private Vector2 moveInput;
    private Rigidbody2D rigidbody;

    #region [Unity Lifecycle Method]
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        HandleInput();
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
        float speed = InputManager.Instance.GetPlayerSprinting() ? runSpeed : walkSpeed;
        rigidbody.linearVelocity = new Vector2(moveInput.x * speed, rigidbody.linearVelocity.y);
    }
    #endregion

    #region [Helper Method]
    private void HandleSpriteFlip() { }
    #endregion
}
