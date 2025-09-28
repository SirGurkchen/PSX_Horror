using System;
using UnityEngine;

public class BusDestroyer : MonoBehaviour
{
    public event Action OnBusDestroy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bus"))
        {
            OnBusDestroy?.Invoke();
        }
    }
}