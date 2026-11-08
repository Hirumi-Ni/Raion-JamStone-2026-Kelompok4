using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    #region Variabels
    [SerializeField] private InteractableEventChannel interactableEvent;
    private readonly List<IInteractable> _nearbyInteractables = new();
    public event Action<IInteractable> OnClosestChanged;
    private IInteractable _closestInteractable;
    public bool PuzzleOpen { get; private set; }
    #endregion

    #region Unity Methods
    private void OnEnable()
    {
        interactableEvent.OnPuzzleRequested += HandlePuzzleOpened;
        interactableEvent.OnClosedRequested += HandlePuzzleClosed;
    }

    private void OnDisable()
    {
        interactableEvent.OnPuzzleRequested -= HandlePuzzleOpened;
        interactableEvent.OnClosedRequested -= HandlePuzzleClosed;
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame || InputManager.Instance.GetPlayerInteract() && PuzzleOpen)
        {
            StopInteraction();
            return;
        }
        // Selalu bersihkan list dari objek yang mungkin dihancurkan
        CleanUpInvalidInteractables();

        if (_nearbyInteractables.Count == 0)
        {
            _closestInteractable = null;
            return;
        }
        GetClosestInteractable();

        if (InputManager.Instance.GetPlayerInteract())
        {
            HandleInteraction();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IInteractable>(out var interact))
        {
            if (!_nearbyInteractables.Contains(interact))
                _nearbyInteractables.Add(interact);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IInteractable>(out var interact))
        {
            _nearbyInteractables.Remove(interact);
            GetClosestInteractable();
        }
    }
    #endregion

    #region Interaction Methods
    private void HandleInteraction()
    {
        // Pakai .Equals(null) untuk mengecek interface yang objeknya mungkin sudah di-Destroy
        if (_closestInteractable == null || _closestInteractable.Equals(null)) return;
        
        IInteractable target = _closestInteractable; 
        
        // Reset state closest interactable SEBELUM interaksi supaya aman dari spam klik
        _closestInteractable = null;
        target.Interaction();         
        CleanUpInvalidInteractables();
        SetClosest(_closestInteractable);
    }
    private void StopInteraction()
    {
        interactableEvent.RaiseCloseRequest();   
    }

    private void GetClosestInteractable()
    {
        IInteractable _closestInteractable = null;
        float currentClosestDistance = float.MaxValue;  
        Vector3 playerPost = this.transform.position;

        foreach (IInteractable interactable in _nearbyInteractables) 
        {
            // Pengecekan krusial menggunakan .Equals(null)
            if (interactable == null || interactable.Equals(null)) continue;
            
            float distance = Vector2.SqrMagnitude(playerPost - interactable.InteractableTransform.position);                
            if (distance < currentClosestDistance)
            {
                _closestInteractable = interactable;
                currentClosestDistance = distance;
            }
        }

        SetClosest(_closestInteractable);
    }

    private void SetClosest(IInteractable next)
    {
        if (next == _closestInteractable) return;
        _closestInteractable = next;
        OnClosestChanged?.Invoke(next);
        Debug.Log("[Set Closest] Method Running");
    }

    private void CleanUpInvalidInteractables()
    {
        // Hapus semua objek yang sudah di-Destroy. 
        // item.Equals(null) akan bernilai TRUE jika gameObject dari IInteractable tersebut sudah hancur.
        _nearbyInteractables.RemoveAll(item => item == null || item.Equals(null));
    }

    private void HandlePuzzleOpened(PuzzleData _) => PuzzleOpen = true;
    private void HandlePuzzleClosed() => PuzzleOpen = false;
    #endregion
}
