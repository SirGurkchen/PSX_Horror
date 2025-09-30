using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CreatureScript : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _neck;
    [SerializeField] private Vector3 _rotationOffset = new Vector3(-90, 0, 0); // Adjust as needed

    private void Update()
    {
        Vector3 targetDirection = _player.transform.position - _neck.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        targetRotation *= Quaternion.Euler(_rotationOffset);
        _neck.transform.rotation = Quaternion.Slerp(_neck.transform.rotation, targetRotation, 5 * Time.deltaTime);
    }
}