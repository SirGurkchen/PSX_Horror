using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuLogic : MonoBehaviour
{
    [SerializeField] private StartMenuLandscapeSpawner _spawner;
    [SerializeField] private MainMenuAudioManager _audioManager;

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
        Application.Quit();
    }

    public void PlayGame()
    {
        LoadingSceneLogic.nextScene = "GameScene";
        SceneManager.LoadScene("LoadingScreenScene");
    }
}