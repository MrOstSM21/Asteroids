using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.View
{
    public enum AudioButtonSpriteName
    {
        MusicOn,
        MusicOff,
        SoundOn,
        SoundOff
    }
    public enum SoundName
    {
        Fire,
        Laser,
        Explosion
    }

    public class SoundView : MonoBehaviour
    {
        public Action ChangeAllSound;
        public Action ChangeMusic;

        [SerializeField] private AudioClip[] _backgroundMusic;
        [SerializeField] private AudioClip _fire;
        [SerializeField] private AudioClip _laser;
        [SerializeField] private AudioClip _explosion;
        [SerializeField] private Image[] _buttonMusicImages;
        [SerializeField] private Image[] _buttonSoundImages;
        [SerializeField] private AudioSource _backgroundAudioSource;
        [SerializeField] private AudioSource _soundAudioSource;
        [SerializeField] private Sprite _soundOn;
        [SerializeField] private Sprite _soundOff;
        [SerializeField] private Sprite _musicOn;
        [SerializeField] private Sprite _musicOff;

        public AudioClip[] GetBackgroundMusic => _backgroundMusic;
        public AudioSource GetBackgroundAudioSource => _backgroundAudioSource;
        public AudioSource GetSoundAudioSource => _soundAudioSource;
        public Image[] GetButtonSoundImage => _buttonSoundImages;
        public Image[] GetButtonMusicImage => _buttonMusicImages;
        public Dictionary<AudioButtonSpriteName, Sprite> GetAudioButtonSprites => new()
        {
            { AudioButtonSpriteName.MusicOn, _musicOn },
            { AudioButtonSpriteName.MusicOff, _musicOff },
            { AudioButtonSpriteName.SoundOn, _soundOn },
            { AudioButtonSpriteName.SoundOff, _soundOff }
        };
        public Dictionary<SoundName, AudioClip> GetSounds => new()
        {
            { SoundName.Fire, _fire },
            { SoundName.Laser, _laser },
            { SoundName.Explosion, _explosion }
        };

        public void SoundChange() => ChangeAllSound?.Invoke();
        public void MusicChange() => ChangeMusic?.Invoke();

      

    }
}
