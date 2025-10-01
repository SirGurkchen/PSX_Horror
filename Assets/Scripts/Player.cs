using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private FirstPersonController _fpController;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private Camera _playerCam;
    [SerializeField] private float _interactDistance = 1.5f;
    [SerializeField] private LayerMask _interactMask;
    [SerializeField] private Transform _holdPoint;
    [SerializeField] private BusLogic _busLogic;

    private enum State { Walking, Running, Standing, InBus, Dead };
    private State _currentState;
    private GameObject _lookInteractable;
    private GameObject _previousInteractable;
    private GameObject _heldObject = null;

    private void Start()
    {
        _currentState = State.Standing;

        _busLogic.OnPlayerHit += _busLogic_OnPlayerHit;
    }

    private void _busLogic_OnPlayerHit()
    {
        SetDead();
    }

    private void Update()
    {
        SetPlayerMoveState();
        HandleWalkingAudio();
        HandleInteract();
        HandleInteractOutline();
    }

    private void SetPlayerMoveState()
    {
        if (_currentState == State.Dead)
        {
            return;
        }

        if (_fpController.isSprinting)
        {
            _currentState = State.Running;
        }
        else if (_fpController.isWalking)
        {
            _currentState = State.Walking;
        }
        else
        {
            if (_currentState != State.InBus)
            {
                _currentState = State.Standing;
            }
        }
    }

    private void HandleWalkingAudio()
    {
        if (_currentState == State.Dead)
        {
            _audioManager.DisableWalkingAudio();
            return;
        }

        bool isMoving = _fpController.isWalking || _fpController.isSprinting;
        bool isWalking = _currentState == State.Walking && isMoving;
        bool isRunning = _currentState == State.Running && isMoving;

        _audioManager.PlayWalkSound(isWalking, isRunning);
    }

    private void HandleInteractOutline()
    {
        if (_currentState != State.InBus)
        {
            _previousInteractable = _lookInteractable;
            _lookInteractable = null;

            if (Physics.Raycast(_playerCam.transform.position, _playerCam.transform.forward, out RaycastHit hit, _interactDistance, _interactMask))
            {
                _lookInteractable = hit.collider.gameObject;
            }

            if (_previousInteractable != _lookInteractable)
            {
                if (_previousInteractable != null && _previousInteractable.TryGetComponent<IInteract>(out IInteract previousInteract))
                {
                    previousInteract.HideOutline();
                }

                if (_lookInteractable != null && _lookInteractable.TryGetComponent<IInteract>(out IInteract currentInteract))
                {
                    currentInteract.ShowOutline();
                }
            }
        }
    }

    private void HandleInteract()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }

    private void TryInteract()
    {
        if (Physics.Raycast(_playerCam.transform.position, _playerCam.transform.forward, out RaycastHit hit, _interactDistance, _interactMask))
        {
            var interactable = hit.collider.GetComponentInParent<IInteract>();
            if (interactable != null)
            {
                interactable.Interact(this);
            }
        }
    }

    public void HoldObject(GameObject newObject)
    {
        if (_heldObject == null)
        {
            _heldObject = newObject;

            if (_heldObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.isKinematic = true;
                rb.detectCollisions = false;
            }

            _heldObject.transform.SetParent(_holdPoint);

            _heldObject.transform.localPosition = Vector3.zero;
            _heldObject.transform.localRotation = Quaternion.Euler(180, 0, 0);
        }
    }

    private void OnDestroy()
    {
        _busLogic.OnPlayerHit -= _busLogic_OnPlayerHit;
    }

    public GameObject GetHeldObject()
    {
        return _heldObject;
    }

    public void SetInBus()
    {
        _currentState = State.InBus;
    }

    private void SetDead()
    {
        _currentState = State.Dead;
    }
    
    public bool PlayerIsStanding()
    {
        return _currentState == State.Standing;
    }

    public void SetAlive()
    {
        _currentState = State.Standing;
    }
}