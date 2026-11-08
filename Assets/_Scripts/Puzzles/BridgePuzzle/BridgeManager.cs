using UnityEngine;

public class BridgeManager : MonoBehaviour
{
    [Header("Bridge Settings")]
    [SerializeField] private Collider2D bridgeCollider; 
    [SerializeField] private Collider2D wallCollider; 
    [SerializeField] private int totalPiecesRequired;
    private int currentPiecesPlaced = 0;

    public void AddPlacedPiece()
    {
        currentPiecesPlaced++;
        if (currentPiecesPlaced >= totalPiecesRequired) CompleteBridge();
    }

    private void CompleteBridge()
    {
        bridgeCollider.enabled = true;
        wallCollider.enabled = false;
    }
}