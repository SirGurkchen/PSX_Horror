using System.Collections.Generic;
using UnityEngine;

public class LandscapePool : MonoBehaviour
{
    public static LandscapePool Instance;

    [SerializeField] private GameObject _landscape;
    [SerializeField] private int _poolSize = 10;

    private Queue<GameObject> _landQueue = new Queue<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        CreatePool(_poolSize);
    }

    private void CreatePool(int size)
    {
        for (int i = 0; i < size; i++)
        {
            var obj = Instantiate(_landscape);
            obj.SetActive(false);
            _landQueue.Enqueue(obj);
        }
    }

    public GameObject GetLand()
    {
        if (_landQueue.Count > 0)
        {
            var land = _landQueue.Dequeue();
            land.SetActive(true);
            return land;
        }
        else
        {
            _poolSize++;
            var obj = Instantiate(_landscape);
            obj.SetActive(false);
            _landQueue.Enqueue(obj);
            var land = _landQueue.Dequeue();
            land.SetActive(true);
            return land;
        }
    }

    public void ReturnLand(GameObject obj)
    {
        obj.SetActive(false);
        _landQueue.Enqueue(obj);
    }
}