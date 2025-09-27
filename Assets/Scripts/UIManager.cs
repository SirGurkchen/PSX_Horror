using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _returnText;

    public void ShowReturnText()
    {
        _returnText.SetActive(true);
    }

    public void DisableReturnText()
    {
        _returnText.SetActive(false);
    }
}