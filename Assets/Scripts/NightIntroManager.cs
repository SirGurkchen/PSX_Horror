using System.Collections;
using System.Threading;
using TMPro;
using UnityEngine;

public class NightIntroManager : MonoBehaviour
{
    [SerializeField] private GameObject _nightUI;
    [SerializeField] private TextMeshProUGUI _nightText;
    [SerializeField] private BusDestroyer _busDestroyer;
    [SerializeField] private NightSO[] _nightSOs;
    [SerializeField] private NightManager _nightManager;


    private int _nightCount = 0;

    private void Start()
    {
        _busDestroyer.OnBusDestroy += _busDestroyer_OnBusDestroy;
    }

    private void _busDestroyer_OnBusDestroy()
    {
        ShowBlack();
        Invoke("ShowNight", 2f);
    }

    public void ShowNight()
    {
        if (_nightCount >= _nightSOs.Length) return;

        ShowNightUI(_nightSOs[_nightCount].nightNumber);
        StartCoroutine(ShowNightScreen(_nightSOs[_nightCount].nightTimer));
    }

    private void ShowBlack()
    {
        _nightText.text = string.Empty;
        _nightUI.SetActive(true);
    }

    private IEnumerator ShowNightScreen(float timer)
    {
        yield return new WaitForSecondsRealtime(timer);

        DisableNightUI();
    }

    private void DisableNightUI()
    {
        _nightUI.SetActive(false);
        Time.timeScale = 1f;
    }

    private void ShowNightUI(int nightNumber)
    {
        _nightUI.SetActive(true);
        _nightText.text = "Night " + nightNumber;
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