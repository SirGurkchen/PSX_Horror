using UnityEngine;

public class BusSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _busObject;
    [SerializeField] private GameObject _busSpawnPoint;

    private void Start()
    {
        _busObject.SetActive(true);
    }
}