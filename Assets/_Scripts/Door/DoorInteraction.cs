using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    [SerializeField] private GameScene _targetScene;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameSceneManager.Instance==null) return;

        if (collision.CompareTag("Player")) GameSceneManager.Instance.ChangeScene(_targetScene);
    }
}
