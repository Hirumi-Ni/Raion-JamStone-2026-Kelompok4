using System;
using UnityEngine;

//This interface symbolize a puzzle at right condition (e.g right position, right rotation, etc) 
public interface ISolveable
{
    //Attach this to any puzzle logic handler object, (e.g. FramePhotoLogic.cs, etc)
    public event Action OnPuzzleSolved;
    public bool IsSolved {get;}
}
