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
    [SerializeField] private BusDoor[] _busDoor;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private Player _player;
    [SerializeField] private const float returnTextTimer = 2f;
    [SerializeField] private CreatureScript _creatureScript;
    [SerializeField] private NightManager _nightManager;

    private int _busDepartCounter = 0;
    private bool _enteredBus = false;

    private void Start()
    {
        // Access Player Borders
        foreach (var border in _borderLogic)
        {
            border.OnBorderHit += Border_OnBorderHit;
        }
        
        // Show Number of Night Screen
        _nightIntroManager.ShowNight();

        // Event if Bus reaches the end of the Street
        _busDestroyer.OnBusDestroy += _busDestroyer_OnBusDestroy;

        // Event if the Bus hits the Player
        _busLogic.OnPlayerHit += _busLogic_OnPlayerHit;

        _player.OnPlayerHitMonster += _player_OnPlayerHit;

        foreach (BusDoor door in _busDoor)
        {
            door.OnBusEnter += _busDoor_OnBusEnter;
        }

        _nightManager.OnNewNight += _nightManager_OnNewNight;
    }

    private void _nightManager_OnNewNight()
    {
        _enteredBus = false;
    }

    private void _busDoor_OnBusEnter()
    {
        _enteredBus = true;
        _busLogic.SetPlayerInBus();

        if (_busDepartCounter >= 6)
        {
            _creatureScript.gameObject.SetActive(false);
        }
    }

    private void _player_OnPlayerHit()
    {
        PlayerDead();
    }

    private void _busLogic_OnPlayerHit()
    {
        PlayerDead();
    }

    private void PlayerDead()
    {
        _uiManager.ShowDeathScreen();
        _audioManager.ChangeWind();

        _busLogic.gameObject.SetActive(false);

        _audioManager.DisableWalkingAudio();

        _creatureScript.StopSound();

        _uiManager.DisableSprintBar();

        Invoke("ReturnToMainMenu", 2f);
    }

    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    private void _busDestroyer_OnBusDestroy()
    {
        _busDepartCounter++;
        CameraManager.Instance.SwitchToPlayerCam();

        if (_busDepartCounter >= 7 || !_enteredBus)
        {
            Invoke("ReturnToMainMenu", 1f);
        }
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

        _busDestroyer.OnBusDestroy -= _busDestroyer_OnBusDestroy;

        _player.OnPlayerHitMonster -= _player_OnPlayerHit;

        foreach (BusDoor door in _busDoor)
        {
            door.OnBusEnter -= _busDoor_OnBusEnter;
        }
        _nightManager.OnNewNight -= _nightManager_OnNewNight;
    }
}