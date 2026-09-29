using System;
using UnityEngine;

public interface ISolveable
{
    //Attach this to any puzzle logic handler object, (e.g. FramePhotoLogic.cs, etc), 
    public event Action OnPuzzleSolved;
    public bool IsSolved {get;}
}
