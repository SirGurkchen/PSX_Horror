using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private FirstPersonController _fpController;
    [SerializeField] private AudioManager _audioManager;

    private enum State { Walking, Running, Standing };
    private State _currentState;

    private void Start()
    {
        _currentState = State.Standing;
    }

    private void Update()
    {
        SetPlayerMoveState();
        HandleWalkingAudio();
    }

    private void SetPlayerMoveState()
    {
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
            _currentState = State.Standing;
        }
    }

    private void HandleWalkingAudio()
    {
        bool isMoving = _fpController.isWalking || _fpController.isSprinting;
        bool isWalking = _currentState == State.Walking && isMoving;
        bool isRunning = _currentState == State.Running && isMoving;

        _audioManager.PlayWalkSound(isWalking, isRunning);
    }
}