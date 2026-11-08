using Ami.BroAudio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    #region [Music/BGM]
    [Header("Music/BGM")]
    public SoundID BGM_Menu;
    public SoundID BGM_Void;
    public SoundID BGM_Wind;
    public SoundID BGM_Ending;
    #endregion

    private SoundID currentBGM;
    private bool hasCurrentBGM;
    private string _lastSceneName;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateBGM(scene.name);
    }

    private void Start()
    {
        if (GameSceneManager.Instance != null)
        {
            UpdateBGM(GameSceneManager.Instance.CurrentScene);
        }
    }

    public void UpdateBGM(string sceneName)
    {
        if (sceneName == _lastSceneName) return;

        SoundID targetBGM;

        if (sceneName == GameScene.Stage1Scene.ToString()) targetBGM = BGM_Void;
        else if (sceneName == GameScene.Stage4Scene.ToString()) targetBGM = BGM_Wind;
        else if (sceneName == GameScene.EndingScene.ToString()) targetBGM = BGM_Ending;
        else if (sceneName == GameScene.MainMenu.ToString()) targetBGM = BGM_Menu;
        else return;

        StopAllBGM();
        BroAudio.Play(targetBGM);
        currentBGM = targetBGM;
        _lastSceneName = sceneName;
    }

    public void StopAllBGM()
    {
        StopAudio(BGM_Menu);
        StopAudio(BGM_Void);
        StopAudio(BGM_Wind);
        StopAudio(BGM_Ending);
    }

    public void PlayAudio(SoundID sound) => BroAudio.Play(sound);
    public void StopAudio(SoundID sound) => BroAudio.Stop(sound);
}