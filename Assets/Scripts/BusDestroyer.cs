using UnityEngine;

public class BusDestroyer : MonoBehaviour
{
    [SerializeField] private BusLogic _busObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bus"))
        {
            _busObject.gameObject.SetActive(false);
        }
    }
}