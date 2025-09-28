using System.Collections;
using UnityEngine;

public class BusLogic : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _busWaitTime = 5f;
    [SerializeField] private float _drivingSpeed = 10f;

    private RigidbodyConstraints _originalConstraints;

    private void Start()
    {
        _rb.linearVelocity = new Vector3(_drivingSpeed, 0f, 0f);
        _originalConstraints = _rb.constraints;
    }

    public void StopBus()
    {
        _rb.linearVelocity = Vector3.zero;
        _rb.constraints = RigidbodyConstraints.FreezeAll;
        StartCoroutine(ResumeBus());
    }

    private IEnumerator ResumeBus()
    {
        yield return new WaitForSeconds(_busWaitTime);

        _rb.constraints = _originalConstraints;
        _rb.linearVelocity = new Vector3(_drivingSpeed, 0f, 0f);
    }
}