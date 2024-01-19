using System;
using _3._Scripts.Architecture;
using UnityEngine;
using UnityEngine.Audio;
using YG;

namespace _3._Scripts.Game
{
    public class AudioManager: Singleton<AudioManager>
    {
        [SerializeField] private AudioYB music;
        [SerializeField] private AudioYB effects;
        
        [SerializeField] private AudioMixer audioMixer;

        public float Volume
        {
            get
            {
                if (audioMixer.GetFloat("SoundsVolume", out var volume))
                    return volume;
                return 0;
            }
            set
            {
                audioMixer.SetFloat("SoundsVolume", value);
                YandexGame.savesData.volume = value;
                YandexGame.SaveProgress();
            }
        }
        private bool _paused;

        protected override void OnAwake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            Volume = YandexGame.savesData.volume;
        }

        public void PlayOneShot(string key, float volume = 0.75f)
        {
            if(_paused) return;
            
            if(YandexGame.nowAdsShow) return;
            
            effects.PlayOneShot(key, volume);
        }

        public void Pause()
        {
            _paused = true;
            
            music.Pause();
            effects.Pause();

            music.volume = 0;
            effects.volume = 0;
        }

        public void Unpause()
        {
            _paused = false;
            
            music.UnPause();
            effects.UnPause();
            
            music.volume = 0.25f;
            effects.volume = 0.75f;
        }

        public void PlayMusic()
        {
            music.loop = true;
            music.volume = 0.25f;

            if (music.isPlaying) return;
            music.Play("background");
        }
    }
}