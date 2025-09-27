using System;
using UnityEngine;

public class BorderLogic : MonoBehaviour
{
    public event Action OnBorderHit;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            OnBorderHit?.Invoke();
        }
    }
}