using UnityEngine;
using DG.Tweening;

public class PuzzlePiece : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform targetPosition;
    [SerializeField] private BridgeManager bridgeManager;

    [Header("Settings")]
    [SerializeField] private float snapDistance = 1.0f;

    private bool isLocked;
    private bool isDragging;

    private Vector3 dragOffset;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (isLocked) return;
        HandleInput();
    }

    private void HandleInput()
    {
        Vector2 mouseScreenPosition = InputManager.Instance.GetMousePosition();
        Vector3 mouseWorldPosition = GetWorldPosition(mouseScreenPosition);

        if (InputManager.Instance.GetLeftClickDown())
        {
            if (IsMouseOverPiece(mouseWorldPosition))
            {
                isDragging = true;
                dragOffset = transform.position - mouseWorldPosition;
            }
        }

        if (isDragging && InputManager.Instance.GetLeftClickHeld())
        {
            transform.position = mouseWorldPosition + dragOffset;
        }

        if (isDragging && !InputManager.Instance.GetLeftClickHeld())
        {
            isDragging = false;
            CheckSnap();
        }
    }

    private bool IsMouseOverPiece(Vector3 mouseWorldPosition)
    {
        Collider2D collider = GetComponent<Collider2D>();
        if (collider == null) return false;
        return collider.OverlapPoint(mouseWorldPosition);
    }

    private void CheckSnap()
    {
        if (targetPosition == null) return;

        float distanceToTarget = Vector2.Distance(transform.position, targetPosition.position);

        if (distanceToTarget <= snapDistance)
        {
            isLocked = true;
            transform.DOMove(targetPosition.position, 0.2f).SetEase(Ease.OutBack).OnComplete(() => bridgeManager.AddPlacedPiece());
        }
    }

    private Vector3 GetWorldPosition(Vector2 screenPosition)
    {
        float distanceFromCamera = Mathf.Abs(transform.position.z - mainCamera.transform.position.z);
        Vector3 screenPosition3D = new Vector3(screenPosition.x, screenPosition.y, distanceFromCamera);
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(screenPosition3D);
        worldPosition.z = transform.position.z;
        return worldPosition;
    }
}