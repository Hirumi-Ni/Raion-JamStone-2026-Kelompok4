using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class DragDropMovement : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, ISolveable
{
    [SerializeField] private float _acceptedOffset;
    [SerializeField] private GameObject _targetHole;
    private RectTransform _rectTransform;
    private Canvas _mainCanvas;
    private CanvasGroup _canvasGroup;
    //Solving
    public bool IsSolved {get; private set;}
    public event Action OnPuzzleSolved;
    
    #region Unity Methods
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _mainCanvas = GetComponentInParent<Canvas>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    #endregion
    #region Interface Implementations
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (IsSolved) return;
       //Lift the photo, trigger naime, etc whatever 
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (IsSolved) return;
       //Move the photo around?
        _rectTransform.anchoredPosition += eventData.delta / _mainCanvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (IsSolved) return;
        //Check offset and autoclip if possible
        if (_targetHole == null) return;
        RectTransform targetPos = _targetHole.GetComponent<RectTransform>();
        if (Vector2.Distance(_rectTransform.position, targetPos.position) < _acceptedOffset)
        {
            _rectTransform.anchoredPosition = targetPos.anchoredPosition;
            IsSolved = true;
            OnPuzzleSolved?.Invoke();
        }
    }
    #endregion
}
