using UnityEngine;

public class LetterScript : MonoBehaviour
{
    public void CloseLetter()
    {
        GameSceneManager.Instance.ChangeScene(GameScene.EndingScene);
    }
}
