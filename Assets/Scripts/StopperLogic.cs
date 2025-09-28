using UnityEngine;

public class StopperLogic : MonoBehaviour
{
    [SerializeField] private BusLogic _busObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bus"))
        {
            _busObject.StopBus();
        }
    }
}