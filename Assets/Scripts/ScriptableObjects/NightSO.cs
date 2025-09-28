using UnityEngine;

[CreateAssetMenu(fileName = "NewNightData", menuName = "ScriptableObjects/Night Data")]
public class NightSO : ScriptableObject
{
    public int nightNumber;
    public float nightTimer;
    public float busSpawnTimer;
    public float busDepartTimer;
}