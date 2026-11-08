using DG.Tweening;
using UnityEngine;

public class InteractionPrompt : MonoBehaviour
{
    [SerializeField] private PlayerInteraction _player;
    [SerializeField] private InteractableEventChannel _eventChannel;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Transform _icon;            // child that bobs up and down
    [SerializeField] private Vector3 _offset = new Vector3(0f, 1.5f, 0f);
    [SerializeField] private float _fadeTime = 0.15f;
    [SerializeField] private float _bobHeight = 0.1f;

    private Transform _target;
    private bool _puzzleOpen;
    private Tween _fade;

    #region Unity Methods
    private void Awake()
    {
        _canvasGroup.alpha = 0f;
        float localBob = _bobHeight / _icon.parent.lossyScale.y;

        _icon.DOLocalMoveY(localBob, 1f)
             .SetLoops(-1, LoopType.Yoyo)
             .SetEase(Ease.InOutSine)
             .SetLink(gameObject);
    }

    private void OnEnable()
    {
        _player.OnClosestChanged += HandleClosestChanged;
        _eventChannel.OnPuzzleRequested += HandlePuzzleOpened;
        _eventChannel.OnClosedRequested += HandlePuzzleClosed;
    }

    private void OnDisable()
    {
        _player.OnClosestChanged -= HandleClosestChanged;
        _eventChannel.OnPuzzleRequested -= HandlePuzzleOpened;
        _eventChannel.OnClosedRequested -= HandlePuzzleClosed;
    }

    private void LateUpdate()
    {
        bool valid = _target != null && _target.gameObject.activeInHierarchy;

        if (!valid)
        {
            _target = null;
            if (_canvasGroup.alpha > 0f) Fade(false);
            return;
        }

        transform.position = _target.position + _offset;
    }
    #endregion

    #region Event Handlers
    private void HandleClosestChanged(IInteractable target)
    {
        _target = target != null ? target.InteractableTransform : null;
        Refresh();
    }

    private void HandlePuzzleOpened(PuzzleData _)
    {
        _puzzleOpen = true;
        Refresh();
    }

    private void HandlePuzzleClosed()
    {
        _puzzleOpen = false;
        Refresh();
    }
    #endregion

    #region Helpers
    private void Refresh()
    {
        bool show = _target != null && !_puzzleOpen;
        Fade(show);
    }

    private void Fade(bool show)
    {
        _fade?.Kill();
        _fade = _canvasGroup.DOFade(show ? 1f : 0f, _fadeTime).SetLink(gameObject);
    }
    #endregion
}