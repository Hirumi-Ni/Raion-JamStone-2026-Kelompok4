using UnityEngine;
using Unity.Cinemachine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }

    [SerializeField] private CinemachineCamera defaultCamera;

    public CinemachineCamera ActiveCamera { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        ActiveCamera = defaultCamera;
        defaultCamera.Priority = 10;
    }

    public void SwitchCamera(CinemachineCamera newCamera)
    {
        if (newCamera == null || newCamera == ActiveCamera)
            return;

        // Disable the currently active camera
        if (ActiveCamera != null)
            ActiveCamera.Priority = 0;

        // Enable the new camera
        newCamera.Priority = 10;

        ActiveCamera = newCamera;
    }

    public void SwitchToDefaultCamera()
    {
        SwitchCamera(defaultCamera);
    }
}
