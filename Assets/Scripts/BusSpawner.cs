using System.Collections;
using UnityEngine;

public class BusSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _busObject;
    [SerializeField] private GameObject _busSpawnPoint;

    public void StartNight(float busTimer)
    {
        StartCoroutine(SpawnBus(busTimer));
    }

    private IEnumerator SpawnBus(float timer)
    {
        yield return new WaitForSeconds(timer);
        _busObject.SetActive(true);
    }
}