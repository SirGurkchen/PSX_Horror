using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuLogic : MonoBehaviour
{
    [SerializeField] private StartMenuLandscapeSpawner _spawner;
    [SerializeField] private MainMenuAudioManager _audioManager;
    [SerializeField] private StartMenuUIManager _UIManager;

    private void Start()
    {
        StartCoroutine(_spawner.SpawnLandscape());
        _audioManager.PlayMusic();
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void QuitGame()
    {
        _audioManager.PlayClick();
        Application.Quit();
    }

    public void PlayGame()
    {
        _audioManager.PlayClick();
        LoadingSceneLogic.nextScene = "GameScene";
        SceneManager.LoadScene("LoadingScreenScene");
    }

    public void ShowControls()
    {
        _audioManager.PlayClick();
        _UIManager.SetControlsActive();
    }

    public void HideControls()
    {
        _audioManager.PlayClick();
        _UIManager.SetControlsDisabled();
    }
}