using UnityEngine;

public class BridgeManager : MonoBehaviour
{
    [Header("Bridge Settings")]
    [SerializeField] private Collider2D bridgeCollider; 
    [SerializeField] private SpriteRenderer bridgeHighlight; 
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
        Color c = bridgeHighlight.color;
        c.a = 1f; 
        bridgeHighlight.color = c;
        Debug.Log("Done");
    }
}