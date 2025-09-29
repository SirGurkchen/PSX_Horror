using System.Collections;
using UnityEngine;

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
        foreach (var border in _borderLogic)
        {
            border.OnBorderHit += Border_OnBorderHit;
        }
        _nightIntroManager.ShowNight();
        _busSpawner.StartNight(_nightIntroManager.GetNight().busSpawnTimer + _nightIntroManager.GetNight().nightTimer);
        _busLogic.SetDepartTimer(_nightIntroManager.GetNight().busDepartTimer);
        _busDestroyer.OnBusDestroy += _busDestroyer_OnBusDestroy;
        _busLogic.OnPlayerHit += _busLogic_OnPlayerHit;
    }

    private void _busLogic_OnPlayerHit()
    {
        _uiManager.ShowBlackScreen();
        _busLogic.gameObject.SetActive(false);
        _audioManager.DisableWalkingAudio();
        _uiManager.DisableSprintBar();
        Time.timeScale = 0f;
        Debug.Log("Death!");
    }

    private void _busDestroyer_OnBusDestroy()
    {
        CameraManager.Instance.SwitchToPlayerCam();
        _busSpawner.StartNight(_nightIntroManager.GetNight().busSpawnTimer + _nightIntroManager.GetNight().nightTimer);
        _busLogic.SetDepartTimer(_nightIntroManager.GetNight().busDepartTimer);
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