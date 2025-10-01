using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private BorderLogic[] _borderLogic;
    [SerializeField] private NightIntroManager _nightIntroManager;
    [SerializeField] private BusSpawner _busSpawner;
    [SerializeField] private BusLogic _busLogic;
    [SerializeField] private BusDestroyer _busDestroyer;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private const float returnTextTimer = 2f;

    private void Start()
    {
        // Acces Player Borders
        foreach (var border in _borderLogic)
        {
            border.OnBorderHit += Border_OnBorderHit;
        }
        
        // Show Number of Night Screen
        _nightIntroManager.ShowNight();

        // Event if Bus reaches the end of the Street
        _busDestroyer.OnBusDestroy += _busDestroyer_OnBusDestroy;

        // Event if the BUs hits the Player
        _busLogic.OnPlayerHit += _busLogic_OnPlayerHit;
    }

    private void _busLogic_OnPlayerHit()
    {
        _uiManager.ShowDeathScreen();
        _audioManager.ChangeWind();

        // Deactivates Bus Object
        _busLogic.gameObject.SetActive(false);

        // Disables Walking Audio of Player (Needed if the player walks/runs while being hit)
        _audioManager.DisableWalkingAudio();

        // Disables Sprint Bar if player sprints while beind hit
        _uiManager.DisableSprintBar();


        Invoke("ReturnToMainMenu", 2f);
    }

    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    private void _busDestroyer_OnBusDestroy()
    {
        CameraManager.Instance.SwitchToPlayerCam();
    }

    private void Border_OnBorderHit()
    {
        StartCoroutine(ShowReturnPrompt());
    }

    public IEnumerator ShowReturnPrompt()
    {
        _uiManager.ShowReturnText();

        yield return new WaitForSeconds(returnTextTimer);

        _uiManager.DisableReturnText();
    }

    private void OnDestroy()
    {
        foreach (var border in _borderLogic)
        {
            border.OnBorderHit -= Border_OnBorderHit;
        }
        _busLogic.OnPlayerHit -= _busLogic_OnPlayerHit;
    }
}