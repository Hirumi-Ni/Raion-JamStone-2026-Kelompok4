using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class RotationInteraction : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, ISolveable
{
#region Variables
    [SerializeField] private RectTransform _frameTransform;
    [SerializeField] private float _targetRotation;
    [SerializeField] private float _acceptedOffset;
    private float _rotationalOffset;

    //checked/Subbed by SolvedHandler 
    public event Action OnPuzzleSolved;
    public bool IsSolved {get; private set;}
    #endregion

    #region Unity Methods
    private void Awake()
    {
        _frameTransform = GetComponent<RectTransform>();
    }
    #endregion

    #region Interface Implementations
    public void OnPointerDown(PointerEventData eventData)
    {
        float initialMouseAngle = GetMouseAngle(eventData);
        _rotationalOffset = initialMouseAngle - _frameTransform.localEulerAngles.z;
    }

    public void OnDrag(PointerEventData eventData)
    {
        float newMouseAngle = GetMouseAngle(eventData);        
        _frameTransform.localRotation = Quaternion.Euler(0, 0, newMouseAngle - _rotationalOffset);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CheckAcceptedRange();
    }
    #endregion

    #region Calculation Logic
    private float GetMouseAngle(PointerEventData eventData)
    {
        Vector2 frameScreenPoint = RectTransformUtility.WorldToScreenPoint(
            eventData.pressEventCamera, 
            _frameTransform.position
        );

        Vector2 direction = eventData.position - frameScreenPoint;
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }

    private void CheckAcceptedRange()
    {
        float currentZ = _frameTransform.localEulerAngles.z;
        float angleDifference = Mathf.Abs(Mathf.DeltaAngle(currentZ, _targetRotation));

        if (Mathf.Abs(angleDifference) <= _acceptedOffset)
        {
            _frameTransform.localRotation = Quaternion.Euler(0, 0, _targetRotation);
            IsSolved = true;
            OnPuzzleSolved?.Invoke();
            Debug.Log($"{this} Sudah sesuai target!");
        } else
        {
            IsSolved = false;
            Debug.Log($"{this} belum sesuai target!");
        }
    }
    #endregion
}
