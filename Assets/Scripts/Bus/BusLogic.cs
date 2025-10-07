using System;
using System.Collections;
using UnityEngine;

public class BusLogic : MonoBehaviour
{
    public event Action OnPlayerHit;

    [SerializeField] private Rigidbody _rb;
    [SerializeField] private BusDestroyer _destroyer;
    [SerializeField] private float _busWaitTime;
    [SerializeField] private float _drivingSpeed = 10f;

    private RigidbodyConstraints _originalConstraints;
    private Vector3 _originalPosition;

    private enum State
    {
        Driving,
        Standing
    }
    private State _currentState;

    private void Start()
    {
        _rb.linearVelocity = new Vector3(_drivingSpeed, 0f, 0f);
        _originalConstraints = _rb.constraints;
        _originalPosition = this.transform.position;
        _currentState = State.Driving;
        _destroyer.OnBusDestroy += _destroyer_OnBusDestroy;
    }

    private void _destroyer_OnBusDestroy()
    {
        DespawnBus();
    }

    public void StopBus()
    {
        _rb.linearVelocity = Vector3.zero;
        _rb.constraints = RigidbodyConstraints.FreezeAll;
        _currentState = State.Standing;
        StartCoroutine(ResumeBus());
    }

    private IEnumerator ResumeBus()
    {
        yield return new WaitForSeconds(_busWaitTime);

        _rb.constraints = _originalConstraints;
        _rb.linearVelocity = new Vector3(_drivingSpeed, 0f, 0f);
        _currentState = State.Driving;
    }

    public void SetDepartTimer(float departTimer)
    {
        _busWaitTime = departTimer;
    }

    private void DespawnBus()
    {
        transform.position = _originalPosition;
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_currentState == State.Driving && collision.gameObject.CompareTag("Player"))
        {
            OnPlayerHit?.Invoke();
        }
    }

    private void OnDestroy()
    {
        _destroyer.OnBusDestroy -= _destroyer_OnBusDestroy;
    }
}