using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [SerializeField] private Camera _playerCam;
    [SerializeField] private Camera _busCamera;

    private void Awake()
    {
        Instance = this;
    }

    public void SwitchToBusCam()
    {
        if (_busCamera != null)
        {
            _playerCam.enabled = false;
            AudioListener playerListener = _playerCam.GetComponent<AudioListener>();
            playerListener.enabled = false;

            _busCamera.enabled = true;
            AudioListener busListener = _busCamera.GetComponent<AudioListener>();
            busListener.enabled = true;
        }
    }

    public void SwitchToPlayerCam()
    {
        if (_playerCam != null)
        {
            _playerCam.enabled = true;
            AudioListener playerListener = _playerCam.GetComponent<AudioListener>();
            playerListener.enabled = true;

            _busCamera.enabled = false;
            AudioListener busListener = _busCamera.GetComponent<AudioListener>();
            busListener.enabled = false;
        }
    }

    public void RegisterBusCamera(Camera busCamera)
    {
        _busCamera = busCamera;
        _busCamera.enabled = false;
        AudioListener busListen = _busCamera.GetComponent<AudioListener>();
        busListen.enabled = false;
    }
}