using UnityEngine;

public class StartMenuUIManager : MonoBehaviour
{
    [SerializeField] private GameObject _controlsScreen;
    [SerializeField] private GameObject _startScreen;

    public void SetControlsActive()
    {
        _controlsScreen.SetActive(true);
        _startScreen.SetActive(false);
    }

    public void SetControlsDisabled()
    {
        _controlsScreen.SetActive(false);
        _startScreen.SetActive(true);
    }
}