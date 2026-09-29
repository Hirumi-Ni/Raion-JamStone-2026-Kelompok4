using System;
using System.Collections.Generic;
using UnityEngine;

public class SolvedHandler : MonoBehaviour
{
    //Can be used for runtime sets to handle every puzzle in an area
    #region Variables
    [SerializeField] private GameObject [] _segmentObject;
    private List<ISolveable>  _segmentsToSolve;
    public event Action OnEverySegmentSolved;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        _segmentsToSolve = new();
        foreach (GameObject obj in _segmentObject)
        {
            _segmentsToSolve.Add(obj.GetComponent<ISolveable>());
        }
    }
    private void OnEnable()
    {
        foreach (ISolveable segment in _segmentsToSolve)
        {
            segment.OnPuzzleSolved += checkWin;
        }
    }
    private void OnDisable()
    {
        foreach (ISolveable segment in _segmentsToSolve)
        {
            segment.OnPuzzleSolved -= checkWin;
        }
    }
    #endregion

    #region 
    private void checkWin()
    {
        if (EverySegmentSolved())
        {
            Debug.Log($"{this} You've solved every segment !");
            return;
        }
        Debug.Log($"{this} You have not complete every segment !!!");
    }
    private bool EverySegmentSolved()
    {
        foreach (ISolveable segment in _segmentsToSolve)
        {
            if (!segment.IsSolved)
            {
                return false;
            }
        }
        OnEverySegmentSolved?.Invoke();
        return true;
    }
    #endregion
}
