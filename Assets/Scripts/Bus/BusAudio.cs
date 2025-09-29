using UnityEngine;

public class BusAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _busAudioSource;
    [SerializeField] private float _maxBusSoundDistance = 50f;
    [SerializeField] private AnimationCurve _volumeRolloff = AnimationCurve.Linear(0f, 1f, 1f, 0f);
    [SerializeField] private AudioClip _engineClip;
    [SerializeField] private Rigidbody _busrb;

    private void Awake()
    {
        SetUpBusAudio();
    }

    private void Update()
    {
        HandleBusAudio();
    }

    private void SetUpBusAudio()
    {
        _busAudioSource.clip = _engineClip;
        _busAudioSource.loop = true;
        _busAudioSource.playOnAwake = false;
        _busAudioSource.spatialBlend = 1f;
        _busAudioSource.rolloffMode = AudioRolloffMode.Custom;
        _busAudioSource.SetCustomCurve(AudioSourceCurveType.CustomRolloff, _volumeRolloff);
        _busAudioSource.maxDistance = _maxBusSoundDistance;
        _busAudioSource.minDistance = 1f;
        _busAudioSource.spread = 45f;
    }

    private void HandleBusAudio()
    {
        bool shouldPlay = _busrb.linearVelocity.magnitude > 0.1f;

        if (shouldPlay && !_busAudioSource.isPlaying)
        {
            _busAudioSource.Play();
        }
        else if (!shouldPlay && _busAudioSource.isPlaying)
        {
            _busAudioSource.Stop();
        }
    }
}