using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _returnText;
    [SerializeField] private TextMeshProUGUI _busText;
    [SerializeField] private TextMeshProUGUI _nightText;
    [SerializeField] private GameObject _nightUI;
    [SerializeField] private GameObject _sprintBar;

    public void ShowReturnText()
    {
        _returnText.SetActive(true);
    }

    public void DisableReturnText()
    {
        _returnText.SetActive(false);
    }

    public void ShowBusText(string text)
    {
        _busText.text = text;
    }

    public void HideBusText()
    {
        _busText.text = string.Empty;
    }

    public void ShowNightUI(int nightNumber)
    {
        _nightUI.SetActive(true);
        _nightText.text = "Night " + nightNumber;
    }

    public void DisableNightUI()
    {
        _nightUI.SetActive(false);
    }

    public void ShowBlackScreen()
    {
        _nightText.text = string.Empty;
        _nightUI.SetActive(true);
    }

    public void DisableSprintBar()
    {
        _sprintBar.SetActive(false);
    }
}