using System.Reflection.Metadata.Ecma335;
using UnityEngine;

public class PuzzleInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private InteractableEventChannel _eventChannel;
    [SerializeField] private PuzzleData _puzzleData;
    public Transform InteractableTransform => this.transform;
    public void Interaction()
    {
        Debug.Log("[PuzzleInteraction] Doing Puzzle Interaction");
        _eventChannel.RaisePuzzleRequest(_puzzleData);
    }
}
