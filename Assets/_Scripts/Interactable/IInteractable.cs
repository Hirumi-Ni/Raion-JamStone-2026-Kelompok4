using UnityEngine;

public interface IInteractable
{
    public Transform InteractableTransform{get;}
    public abstract void Interaction();
}
