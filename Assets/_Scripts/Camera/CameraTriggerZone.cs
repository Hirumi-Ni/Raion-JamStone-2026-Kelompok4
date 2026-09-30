using Unity.Cinemachine;
using UnityEngine;

public class CameraTriggerZone : MonoBehaviour
{
    [SerializeField] private CinemachineCamera zoneCamera;

    private void Start()
    {
        zoneCamera.Priority = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CameraController.Instance.SwitchCamera(zoneCamera);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CameraController.Instance.SwitchToDefaultCamera();
        }
    }
}
