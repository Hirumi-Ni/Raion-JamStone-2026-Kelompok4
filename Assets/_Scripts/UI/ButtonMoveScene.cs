using UnityEngine;

public class ButtonMoveScene : MonoBehaviour
{
    [SerializeField] private GameScene _targetScene;
    public void ChangeScene()
    {
        if (GameSceneManager.Instance == null) return;
        GameSceneManager.Instance.ChangeScene(_targetScene);
    }
}
