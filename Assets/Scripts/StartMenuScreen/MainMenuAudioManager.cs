using System.Collections.Generic;
using UnityEngine;
using static AudioManager;

[System.Serializable]
public class MenuSound
{
    public MainMenuAudioManager.MenuSoundType type;
    public AudioClip clip;
}

public class MainMenuAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _menuMusicSource;
    [SerializeField] private AudioSource _clickSource;
    [SerializeField] private List<MenuSound> _menuSounds;

    private Dictionary<MenuSoundType, AudioClip> _menuSoundDic;

    public enum MenuSoundType
    {
        Music,
        Click
    }

    private void Awake()
    {
        if (_menuSoundDic == null)
        {
            BuildDictionary();
        }
    }

    private void BuildDictionary()
    {
        _menuSoundDic = new Dictionary<MenuSoundType, AudioClip>();
        foreach (MenuSound s in _menuSounds)
        {
            _menuSoundDic[s.type] = s.clip;
        }
    }

    public void PlayMusic()
    {
        _menuMusicSource.clip = _menuSoundDic[MenuSoundType.Music];
        _menuMusicSource.loop = true;
        _menuMusicSource.Play();
    }

    public void PlayClick()
    {
        _clickSource.PlayOneShot(_menuSoundDic[MenuSoundType.Click]);
    }
}