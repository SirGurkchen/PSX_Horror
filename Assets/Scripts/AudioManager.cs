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
    [SerializeField] private List<Sound> _sounds;

    private Dictionary<SoundType, AudioClip> _soundDic;

    public enum SoundType
    {
        Walking,
        Running
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
}