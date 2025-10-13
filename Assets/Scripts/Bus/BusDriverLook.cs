using UnityEngine;

public class BusDriverLook : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    private void Update()
    {
        Vector3 targetDirection = _player.transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);
    }
}