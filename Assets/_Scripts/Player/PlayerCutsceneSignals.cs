using UnityEngine;

public class PlayerCutsceneSignals : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Rigidbody2D rb;
    [SerializeField] private Transform destination;
    [SerializeField] private float speed = 3f;

    private bool isMoving;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!isMoving) return;

        Vector2 newPosition = Vector2.MoveTowards(rb.position, destination.position, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);

        if (Mathf.Abs(newPosition.x - destination.position.x) < 0.01f)
        {
            isMoving = false;
            playerMovement.SetCutsceneMoving(false);
            Debug.Log("Reached");
        }
    }

    public void StartPlayerMovement()
    {
        isMoving = true;
        playerMovement.SetCutsceneMoving(isMoving);
    }
}
