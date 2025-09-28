using System;
using UnityEngine;

public class BusDestroyer : MonoBehaviour
{
    public event Action OnBusDestroy;

    [SerializeField] private NightIntroManager _intro;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bus"))
        {
            _intro.IncreaseNight();
            OnBusDestroy?.Invoke();
        }
    }
}