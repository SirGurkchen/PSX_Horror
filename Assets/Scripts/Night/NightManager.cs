using System.Collections;
using UnityEngine;

public class NightManager : MonoBehaviour
{
    [SerializeField] private NightIntroManager _intro;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private float _textTimer;
    [SerializeField] private GameObject _moonBlocker;
    [SerializeField] private Player _player;
    [SerializeField] private Transform _playerStartPos;
    [SerializeField] private GameObject _canTrash;
    [SerializeField] private BusSpawner _busSpawner;
    [SerializeField] private BusLogic _busLogic;
    [SerializeField] private NightIntroManager _nightIntroManager;
    [SerializeField] private TrashCanLogic _trashCanLogic;

    private void Start()
    {
        _trashCanLogic.OnAllTrashCollected += _trashCanLogic_OnAllTrashCollected;
    }

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
            case 3:
                PlayNightThree(night);
                break;
        }
    }

    private void PlayNightOne(NightSO night)
    {
        SpawnBus();
        StartCoroutine(ShowNightText(night));
    }

    private void PlayNightTwo(NightSO night)
    {
        MoveEarthObject(night.moonBlockerOffset);
        SpawnBus();
        StartCoroutine(ShowNightText(night));
    }

    private void PlayNightThree(NightSO night)
    {
        MoveEarthObject(night.moonBlockerOffset);
        ActivateTrash();
        StartCoroutine(ShowNightText(night));
    }

    private void _trashCanLogic_OnAllTrashCollected()
    {
        SpawnBus();
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
        _player.gameObject.transform.position = _playerStartPos.position;
        _player.gameObject.transform.rotation = Quaternion.identity;
    }

    private void ActivateTrash()
    {
        _canTrash.SetActive(true);
        _trashCanLogic.ActivateTrashCan();
    }

    private void OnDestroy()
    {
        _trashCanLogic.OnAllTrashCollected -= _trashCanLogic_OnAllTrashCollected;
    }

    private void SpawnBus()
    {
        _busSpawner.StartNight(_nightIntroManager.GetNight().busSpawnTimer + _nightIntroManager.GetNight().nightTimer);
        _busLogic.SetDepartTimer(_nightIntroManager.GetNight().busDepartTimer);
    }
}