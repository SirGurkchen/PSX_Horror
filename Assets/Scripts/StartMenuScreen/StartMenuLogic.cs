using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuLogic : MonoBehaviour
{
    [SerializeField] private StartMenuLandscapeSpawner _spawner;

    private void Start()
    {
        StartCoroutine(_spawner.SpawnLandscape());
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