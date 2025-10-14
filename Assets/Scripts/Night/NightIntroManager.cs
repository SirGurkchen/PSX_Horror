using System.Collections;
using UnityEngine;

public class NightIntroManager : MonoBehaviour
{
    [SerializeField] private BusDestroyer _busDestroyer;
    [SerializeField] private NightSO[] _nightSOs;
    [SerializeField] private NightManager _nightManager;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private Player _player;


    private int _nightCount = 0;

    private void Start()
    {
        _busDestroyer.OnBusDestroy += _busDestroyer_OnBusDestroy;
    }

    private void _busDestroyer_OnBusDestroy()
    {
        _uiManager.ShowBlackScreen();
        _nightManager.ChangeWindState();
        Invoke("ShowNight", 2f);
    }

    public void ShowNight()
    {
        if (_nightCount >= _nightSOs.Length) return;

        StartNight(_nightSOs[_nightCount].nightNumber);
        StartCoroutine(ShowNightScreen(_nightSOs[_nightCount].nightTimer));
    }

    private IEnumerator ShowNightScreen(float timer)
    {
        yield return new WaitForSecondsRealtime(timer);

        DisableNightIntro();
        _player.SetAlive();
    }

    private void DisableNightIntro()
    {
        _uiManager.DisableNightUI();
        Time.timeScale = 1f;
        _nightManager.ChangeWindState();
    }

    private void StartNight(int nightNumber)
    {
        _uiManager.ShowNightUI(nightNumber);
        _nightManager.ChooseNight(_nightSOs[_nightCount]);
        Time.timeScale = 0f;
    }

    public NightSO GetNight()
    {
        return _nightSOs[_nightCount];
    }

    public void IncreaseNight()
    {
        _nightCount++;
    }

    private void OnDestroy()
    {
        _busDestroyer.OnBusDestroy -= _busDestroyer_OnBusDestroy;
    }
}