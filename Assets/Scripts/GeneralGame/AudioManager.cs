using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioManager.SoundType type;
    public AudioClip clip;
}
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _walkingAudioSource;
    [SerializeField] private AudioSource _soundsSource;
    [SerializeField] private AudioSource _branchSource;
    [SerializeField] private List<Sound> _sounds;
    [SerializeField] private AnimationCurve _volumeRolloff = AnimationCurve.Linear(0f, 1f, 1f, 0f);

    private Dictionary<SoundType, AudioClip> _soundDic;

    public enum SoundType
    {
        Walking,
        Running,
        Trash,
        Pick,
        Branch
    }

    private void Awake()
    {
        if (_soundDic == null)
        {
            BuildDictionary();
        }
    }

    private void BuildDictionary()
    {
        _soundDic = new Dictionary<SoundType, AudioClip>();
        foreach (Sound s in _sounds)
        {
            _soundDic[s.type] = s.clip;
        }
    }

    public void PlayWalkSound(bool isWalking, bool isRunning)
    {
        if (isWalking)
        {
            if (_walkingAudioSource.clip != _soundDic[SoundType.Walking])
            {
                _walkingAudioSource.clip = _soundDic[SoundType.Walking];
                _walkingAudioSource.loop = true;
                _walkingAudioSource.Play();
            }
        }
        else if (isRunning)
        {
            if (_walkingAudioSource.clip != _soundDic[SoundType.Running])
            {
                _walkingAudioSource.clip = _soundDic[SoundType.Running];
                _walkingAudioSource.loop = true;
                _walkingAudioSource.Play();
            }
        }
        else
        {
            if (_walkingAudioSource.isPlaying)
            {
                _walkingAudioSource.clip = null;
                _walkingAudioSource.Stop();
            }
        }
    }

    public void DisableWalkingAudio()
    {
        _walkingAudioSource.Stop();
    }

    public void PlaySound(SoundType sound)
    {
        _soundsSource.PlayOneShot(_soundDic[sound]);
    }

    public void PlayBreakSound()
    {
        _branchSource.clip = _soundDic[SoundType.Branch];
        _branchSource.loop = false;
        _branchSource.playOnAwake = false;
        _branchSource.spatialBlend = 1f;
        _branchSource.rolloffMode = AudioRolloffMode.Custom;
        _branchSource.SetCustomCurve(AudioSourceCurveType.CustomRolloff, _volumeRolloff);
        _branchSource.maxDistance = 100f;
        _branchSource.minDistance = 1f;
        _branchSource.spread = 45f;

        _branchSource.Play();
    }
}