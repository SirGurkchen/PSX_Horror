using System.Collections;
using UnityEngine;

public class StartMenuLandscapeSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnTimer;

    public IEnumerator SpawnLandscape()
    {
        while (true)
        {
            GameObject land = LandscapePool.Instance.GetLand();

            land.transform.position = this.transform.position;

            yield return new WaitForSeconds(_spawnTimer);
        }
    }
}