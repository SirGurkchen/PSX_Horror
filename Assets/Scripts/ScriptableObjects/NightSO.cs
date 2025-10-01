using UnityEngine;

[CreateAssetMenu(fileName = "NewNightData", menuName = "ScriptableObjects/Night Data")]
public class NightSO : ScriptableObject
{
    public int nightNumber;
    public string nightText;
    public float nightTimer;
    public float busSpawnTimer;
    public float busDepartTimer;
    public float moonBlockerOffset;
    public float monsterSpawnTime;
    public GameObject monsterSpawnPoint;
}