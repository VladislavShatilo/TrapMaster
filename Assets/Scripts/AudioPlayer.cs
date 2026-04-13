using System;
using UnityEngine;

namespace TrapMaster
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource trapSoundSource;

        private bool isMusicOn;
        private bool isTrapSoundOn;

        private void Awake()
        {
            if (musicSource == null || trapSoundSource == null)
            {
                Debug.LogError("AudioSources are not assigned", this);
                enabled = false;
            }
        }

        public void ChangeVolume(float volume)
        {
            musicSource.volume = volume;
            trapSoundSource.volume = volume;
        }

        public void ChangeMusicOn(bool isOn)
        {
            isMusicOn = isOn;

            if (isOn)
                musicSource.Play();
            else
                musicSource.Stop();
        }

        public void ChangeTrapSoundOn(bool isOn)
        {
            isTrapSoundOn = isOn;

            if (isOn)
                trapSoundSource.Play();
            else
                trapSoundSource.Stop();
        }

        public void PlayTrapSound()
        {
            if (!isTrapSoundOn)
                return;

            trapSoundSource.Play();
        }
    }
}