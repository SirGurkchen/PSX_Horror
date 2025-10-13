using UnityEngine;

public class CreatureScript : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _neck;
    [SerializeField] private Vector3 _rotationOffset = new Vector3(-90, 0, 0);
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private AnimationCurve _volumeRolloff = AnimationCurve.Linear(0f, 1f, 1f, 0f);
    [SerializeField] private float _maxAudioDistance = 50f;


    private bool _isChasing = false;

    private void Update()
    {
        Vector3 targetDirection = _player.transform.position - _neck.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        targetRotation *= Quaternion.Euler(_rotationOffset);
        _neck.transform.rotation = Quaternion.Slerp(_neck.transform.rotation, targetRotation, 5 * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (_isChasing)
        {
            ChasePlayer();
        }
    }

    private void ChasePlayer()
    {
        Vector3 dir = _player.transform.position - transform.position;
        dir.y = 0;
        dir.Normalize();

        if (dir.sqrMagnitude > 0.001f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(dir, Vector3.up);
            lookRotation *= Quaternion.Euler(0, 90, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 5f * Time.fixedDeltaTime);
        }

        Vector3 move = dir * _moveSpeed * Time.fixedDeltaTime;
        _rb.MovePosition(_rb.position + move);
    }

    public void StartChase()
    {
        _isChasing = true;
        StartSound();
    }

    private void StartSound()
    {
        _audioSource.clip = _audioClip;
        _audioSource.loop = true;
        _audioSource.playOnAwake = false;
        _audioSource.spatialBlend = 1f;
        _audioSource.rolloffMode = AudioRolloffMode.Custom;
        _audioSource.SetCustomCurve(AudioSourceCurveType.CustomRolloff, _volumeRolloff);
        _audioSource.maxDistance = _maxAudioDistance;
        _audioSource.minDistance = 1f;
        _audioSource.spread = 45f;

        _audioSource.Play();
    }

    public void StopSound()
    {
        _audioSource.Stop();
    }
}