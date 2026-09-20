using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Interactable Event", menuName = "ScriptableObjects/Interactable Event")]
public class InteractableEventChannel : ScriptableObject
{
    public event Action<PuzzleData> OnPuzzleRequested;
    public event Action OnClosedRequested;
    public void RaisePuzzleRequest(PuzzleData _data)
    {
        OnPuzzleRequested?.Invoke(_data);
    }
    public void RaiseCloseRequest()
    {
        OnClosedRequested?.Invoke();
    }

}
