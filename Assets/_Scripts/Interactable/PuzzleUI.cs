using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Search;
using UnityEngine;

public class PuzzleUI : MonoBehaviour
{
    #region Variables
    [SerializeField] private InteractableEventChannel _eventChannel;
    [SerializeField ] private GameObject _currentActivePuzzle; 
    [SerializeField] private Transform _parentPos;
    private readonly Dictionary<PuzzleData, GameObject> _panelCache = new Dictionary<PuzzleData, GameObject>(); 
    #endregion    
    #region Unity Methods
    private void OnEnable()
    {
        _eventChannel.OnPuzzleRequested += ShowUI;
        _eventChannel.OnClosedRequested += CloseUI;
    }
    private void OnDisable()
    {
        _eventChannel.OnPuzzleRequested -= ShowUI;
        _eventChannel.OnClosedRequested-= CloseUI;
    }
    #endregion
    #region UI Handler Methods
    private void ShowUI(PuzzleData _data)
    {
        Debug.Log("[PuzzleUI] Interaction terjadi, tampilkan UI");
        // Hide current panel (klo ada), update current puzzle, show current puzzle
        if (_currentActivePuzzle != null)
        {
            _currentActivePuzzle.SetActive(false);
        }
        if (!_panelCache.TryGetValue(_data, out _currentActivePuzzle))
        {
            _currentActivePuzzle = Instantiate (_data.PanelPrefab, _parentPos);
            _panelCache.Add(_data, _currentActivePuzzle);
        } else
        {
            _currentActivePuzzle.SetActive(true);
        }
    }
    private void CloseUI()
    {
        if (_currentActivePuzzle != null)
        {
            _currentActivePuzzle.SetActive(false);
            _currentActivePuzzle = null;
        }
    }
    #endregion
}
