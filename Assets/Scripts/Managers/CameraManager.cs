using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : PersistentManager<CameraManager>
{
    [SerializeField]
    Collider2D confiner;
    CinemachineVirtualCameraBase cinemachineVirtualCamera;

    void Start()
    {
        cinemachineVirtualCamera = FindAnyObjectByType<CinemachineVirtualCameraBase>();
        if (cinemachineVirtualCamera == null)
        {
            Debug.LogError("Cinemachine not found");
        }
    }

    public void ChangeScene(Vector2 pos, Collider2D confiner)
    {
        cinemachineVirtualCamera = FindAnyObjectByType<CinemachineVirtualCameraBase>();
        if (cinemachineVirtualCamera == null)
        {
            Debug.LogError("Cinemachine not found");
        }

        // cinemachineVirtualCamera.transform.position = pos;
        this.confiner = confiner;
        cinemachineVirtualCamera.GetComponent<CinemachineConfiner2D>().BoundingShape2D = confiner;
    }

    public void SetActiveCinemachineCamera(bool value)
    {
        cinemachineVirtualCamera = FindAnyObjectByType<CinemachineVirtualCameraBase>();
        if (cinemachineVirtualCamera == null)
        {
            Debug.LogError("Cinemachine not found");
        }
        cinemachineVirtualCamera.enabled = value;
    }
}
