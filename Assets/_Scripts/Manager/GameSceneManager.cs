//using EasyTransition;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameScene
{
    MainMenu,
    SampleScene,
    //isi aja scene mu bebas
}

public class GameSceneManager : MonoBehaviour
{
    //[SerializeField] private TransitionSettings transitionMode;
    //[SerializeField] private float loadDelay;

    public static GameSceneManager Instance { get; private set; }

    public string CurrentScene => SceneManager.GetActiveScene().name;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    public void ChangeScene(GameScene sceneEnum)
    {
        SceneManager.LoadScene(sceneEnum.ToString());
        //TransitionManager.Instance().Transition(sceneEnum.ToString(), transitionMode, loadDelay);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
        //TransitionManager.Instance().Transition(SceneManager.GetActiveScene().name, transitionMode, loadDelay);
    }
}