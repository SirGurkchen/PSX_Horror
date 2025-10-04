using System.Collections;
using UnityEngine;

public class NightManager : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private float _textTimer;
    [SerializeField] private GameObject _moonBlocker;
    [SerializeField] private GameObject _moon;
    [SerializeField] private Player _player;
    [SerializeField] private Transform _playerStartPos;
    [SerializeField] private GameObject _canTrash;
    [SerializeField] private BusSpawner _busSpawner;
    [SerializeField] private BusLogic _busLogic;
    [SerializeField] private NightIntroManager _nightIntroManager;
    [SerializeField] private TrashCanLogic _trashCanLogic;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private MonsterSpawner _monsterSpawner;
    [SerializeField] private ChaseMonsterSpawner _chaseSpawner;
    [SerializeField] private GameObject _hangedPerson;
    [SerializeField] private CreatureScript _creature;

    private void Start()
    {
        _trashCanLogic.OnAllTrashCollected += _trashCanLogic_OnAllTrashCollected;
    }

    public void ChooseNight(NightSO night)
    {
        ResetPlayerPosition();
        _player.SetAlive();

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
            case 4:
                PlayNightFour(night);
                break;
            case 5:
                PlayNightFive(night);
                break;
            case 6:
                PlayNightSix(night);
                break;
            case 7:
                PlayNightSeven(night);
                break;
        }
    }

    private void PlayNightOne(NightSO night)
    {
        StartCoroutine(SpawnChasingMonster(night));
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

    private void PlayNightFour(NightSO night)
    {
        MoveEarthObject(night.moonBlockerOffset);
        SpawnBus();
        StartCoroutine(ShowNightText(night));
        StartCoroutine(SpawnMonster(night));
    }

    private void PlayNightFive(NightSO night)
    {
        SpawnDeadPerson();
        MoveEarthObject(night.moonBlockerOffset);
        SpawnBus();
        StartCoroutine(ShowNightText(night));
    }

    private void PlayNightSix(NightSO night)
    {
        ChangeMoonToRed();
        SpawnBus();
        StartCoroutine(ShowNightText(night));
    }

    private void PlayNightSeven(NightSO night)
    {
        SpawnBus();
        StartCoroutine(ShowNightText(night));
        StartCoroutine(SpawnChasingMonster(night));
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

    private IEnumerator SpawnMonster(NightSO night)
    {
        if (night.monsterSpawnPoint != null)
        {
            yield return new WaitForSeconds(night.monsterSpawnTime);

            _monsterSpawner.SpawnMonster(night.monsterSpawnPoint);
        }
    }

    private IEnumerator SpawnChasingMonster(NightSO night)
    {
        if (night.monsterSpawnPoint != null)
        {
            yield return new WaitForSeconds(night.monsterSpawnTime);

            _chaseSpawner.SpawnMonster(night.monsterSpawnPoint);

            _creature.StartChase();
        }
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

    private void SpawnDeadPerson()
    {
        _hangedPerson.SetActive(true);
    }

    private void ChangeMoonToRed()
    {
        Renderer rend = _moon.GetComponent<Renderer>();

        rend.material.EnableKeyword("_EMISSION");

        rend.material.SetColor("_EmissionColor", Color.red);
    }

    public void ChangeWindState()
    {
        _audioManager.ChangeWind();
    }
}