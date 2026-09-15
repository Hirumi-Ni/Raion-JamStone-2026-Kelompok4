using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Puzzle Data", menuName = "ScriptableObjects/Puzzle Data")]
public class PuzzleData : ScriptableObject
{
    public GameObject PanelPrefab => _panelPrefab;
    [SerializeField] private GameObject _panelPrefab;
    [SerializeField] private String _puzzleName;
}
