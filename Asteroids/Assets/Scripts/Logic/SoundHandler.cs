using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.View;
using UnityEngine.UI;

namespace Assets.Scripts.Logic
{
    public class SoundHandler
    {
        public bool GetMusicState => _backgroundAudioSource.mute;

        private readonly SoundView _soundView;
        private readonly UpdateHandler _updateHandler;
        private readonly AudioSource _backgroundAudioSource;
        private readonly AudioClip[] _backgroundMusic;
        private readonly Image[] _buttonMusicImages;
        private readonly Image[] _buttonSoundImages;
        private readonly Dictionary<AudioButtonSpriteName, Sprite> _audioButtonSprites;
        private readonly Dictionary<SoundName, AudioClip> _sounds;

        private int _soundIndex = 0;
        private int _soundNextIndex = 0;


        public SoundHandler(SoundView soundView, UpdateHandler updateHandler)
        {
            _soundView = soundView;
            _sounds = _soundView.GetSounds;
            _backgroundAudioSource = _soundView.GetBackgroundAudioSource;
            _backgroundMusic = _soundView.GetBackgroundMusic;
            _buttonMusicImages = _soundView.GetButtonMusicImage;
            _buttonSoundImages = _soundView.GetButtonSoundImage;
            _audioButtonSprites = _soundView.GetAudioButtonSprites;
            _updateHandler = updateHandler;
            Subscribe();
        }

        public void PlaySound(SoundName soundName)
        {
            switch (soundName)
            {
                case SoundName.Fire:
                    _soundView.GetSoundAudioSource.PlayOneShot(_sounds[soundName]);
                    break;
                case SoundName.Laser:
                    _soundView.GetSoundAudioSource.PlayOneShot(_sounds[soundName]);
                    break;
                case SoundName.Explosion:
                    _soundView.GetSoundAudioSource.PlayOneShot(_sounds[soundName]);
                    break;
                default:
                    break;
            }
        }
        public void SetStartParameters(bool music, bool sound)
        {

            _backgroundAudioSource.mute = music;
            AudioListener.pause = sound;
            if (music)
                ChangeSpriteButton(_buttonMusicImages, _audioButtonSprites[AudioButtonSpriteName.MusicOff]);
            else
                ChangeSpriteButton(_buttonMusicImages, _audioButtonSprites[AudioButtonSpriteName.MusicOn]);

            if (sound)
                ChangeSpriteButton(_buttonSoundImages, _audioButtonSprites[AudioButtonSpriteName.SoundOff]);
            else
                ChangeSpriteButton(_buttonSoundImages, _audioButtonSprites[AudioButtonSpriteName.SoundOn]);

        }

        private void ChangeStateMusic()
        {
            if (!_backgroundAudioSource.mute)
            {
                _backgroundAudioSource.mute = true;
                ChangeSpriteButton(_buttonMusicImages, _audioButtonSprites[AudioButtonSpriteName.MusicOff]);
            }
            else
            {
                _backgroundAudioSource.mute = false;
                ChangeSpriteButton(_buttonMusicImages, _audioButtonSprites[AudioButtonSpriteName.MusicOn]);
            }

        }

        private void ChangeStateAllSound()
        {
            if (!AudioListener.pause)
            {
                AudioListener.pause = true;
                ChangeSpriteButton(_buttonSoundImages, _audioButtonSprites[AudioButtonSpriteName.SoundOff]);
            }
            else
            {
                AudioListener.pause = false;
                ChangeSpriteButton(_buttonSoundImages, _audioButtonSprites[AudioButtonSpriteName.SoundOn]);
            }
        }

        private void Update()
        {
            PlayBackgroundMusic();
        }

        private void PlayNextBackgroundMusic()
        {
            while (_soundIndex == _soundNextIndex)
            {
                _soundNextIndex = Random.Range(0, _backgroundMusic.Length);
            }
            _soundIndex = _soundNextIndex;
            _backgroundAudioSource.PlayOneShot(_backgroundMusic[_soundIndex]);
        }

        private void ChangeSpriteButton(Image[] buttons, Sprite sprite)
        {
            foreach (var item in buttons)
            {
                item.sprite = sprite;
            }
        }
        private void PlayBackgroundMusic()
        {
            if (!_backgroundAudioSource.isPlaying)
            {
                PlayNextBackgroundMusic();
            }
        }
        private void EndGame() => Unsubscribe();

        private void Subscribe()
        {
            _updateHandler.EndGame += EndGame;
            _updateHandler.Update += Update;
            _soundView.ChangeMusic += ChangeStateMusic;
            _soundView.ChangeAllSound += ChangeStateAllSound;
        }

        private void Unsubscribe()
        {
            _updateHandler.EndGame -= EndGame;
            _updateHandler.Update -= Update;
            _soundView.ChangeMusic -= ChangeStateMusic;
            _soundView.ChangeAllSound -= ChangeStateAllSound;
        }

    }
}
