using UnityEngine;

public class ChaseMonsterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _monster;

    public void SpawnMonster(GameObject spawnLocation)
    {
        _monster.SetActive(true);
    }

    public void DespawnMonster()
    {
        _monster.SetActive(false);
    }
}