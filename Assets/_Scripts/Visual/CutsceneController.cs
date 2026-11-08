using UnityEngine;
using UnityEngine.Playables;

public class CutsceneController : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;

    private bool hasPlayed = false;

    private void Start()
    {
        director.playOnAwake = false;
        director.stopped += OnCutsceneFinished;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasPlayed || !other.CompareTag("Player")) return;

        hasPlayed = true;

        other.GetComponent<PlayerMovement>()?.LockMovement();

        StartCutscene();
    }

    private void StartCutscene()
    {
        director.Play();
    }

    private void OnCutsceneFinished(PlayableDirector playableDirector)
    {
        GameSceneManager.Instance.ChangeScene(GameScene.MainMenu);
    }

    private void OnDestroy()
    {
        if (director != null) director.stopped -= OnCutsceneFinished;
    }
}
