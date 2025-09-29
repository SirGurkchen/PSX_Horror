using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuLogic : MonoBehaviour
{
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