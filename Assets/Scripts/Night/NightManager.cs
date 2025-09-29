using System.Collections;
using UnityEngine;

public class NightManager : MonoBehaviour
{
    [SerializeField] private NightIntroManager _intro;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private float _textTimer;
    [SerializeField] private GameObject _moonBlocker;
    [SerializeField] private GameObject _playerObject;
    [SerializeField] private Transform _playerStartPos;

    public void ChooseNight(NightSO night)
    {
        ResetPlayerPosition();

        switch (night.nightNumber)
        {
            case 1:
                PlayNightOne(night);
                break;
            case 2:
                PlayNightTwo(night);
                break;
        }
    }

    private void PlayNightOne(NightSO night)
    {
        StartCoroutine(ShowNightText(night));
    }

    private void PlayNightTwo(NightSO night)
    {
        MoveEarthObject(night.moonBlockerOffset);
        StartCoroutine(ShowNightText(night));
    }

    private IEnumerator ShowNightText(NightSO night)
    {
        _uiManager.ShowBusText(night.nightText);

        yield return new WaitForSeconds(_textTimer);

        _uiManager.HideBusText();
    }

    private void MoveEarthObject(float offset)
    {
        _moonBlocker.transform.position += new Vector3(0, offset, 0);
    }

    private void ResetPlayerPosition()
    {
        _playerObject.transform.position = _playerStartPos.position;
        _playerObject.transform.rotation = Quaternion.identity;
    }
}