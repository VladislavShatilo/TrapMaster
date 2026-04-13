using System;
using UnityEngine;

namespace TrapMaster
{
    public class Sound
    {
        public event Action<float> VolumeChanged;
        public event Action<bool> MusicChanged;
        public event Action<bool> TrapSoundChanged;
        public event Action Loaded;

        public float Volume { get; private set; }
        public bool IsMusicOn { get; private set; }
        public bool IsTrapSoundOn { get; private set; }

        public void SetVolume(float volume)
        {
            ValidateVolume(volume);

            if (Mathf.Approximately(Volume, volume))
                return;

            Volume = volume;
            VolumeChanged?.Invoke(Volume);
        }

        public void SetMusicOn(bool value)
        {
            if (IsMusicOn == value)
                return;

            IsMusicOn = value;
            MusicChanged?.Invoke(IsMusicOn);
        }

        public void SetTrapSoundOn(bool value)
        {
            if (IsTrapSoundOn == value)
                return;

            IsTrapSoundOn = value;
            TrapSoundChanged?.Invoke(IsTrapSoundOn);
        }

        public void LoadSound(float volume, bool isMusicOn, bool isTrapSoundOn)
        {
            SetVolume(volume);
            SetMusicOn(isMusicOn);
            SetTrapSoundOn(isTrapSoundOn);

            Loaded?.Invoke();
        }

        private void ValidateVolume(float volume)
        {
            if (volume < 0f || volume > 1f)
                throw new ArgumentOutOfRangeException(nameof(volume), "Volume должен быть от 0 до 1");
        }
    }
}