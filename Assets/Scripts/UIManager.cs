using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _returnText;
    [SerializeField] private TextMeshProUGUI _busText;

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
}