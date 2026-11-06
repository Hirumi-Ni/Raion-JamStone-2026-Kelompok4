using UnityEngine;

public class DoorInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private GameScene _targetScene;

    public Transform InteractableTransform => this.transform;
    public void Interaction()
    {
        if (GameSceneManager.Instance==null) return;
        GameSceneManager.Instance.ChangeScene(_targetScene);
    }
}
