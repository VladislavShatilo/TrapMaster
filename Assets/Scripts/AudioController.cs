using System;
using Zenject;

namespace TrapMaster
{
    public class AudioController : IInitializable, IDisposable
    {
        private AudioPlayer audioPlayer;
        private Sound sound;
        private SettingsPopupView settingsPopupView;

        [Inject]
        public void Construct(
            AudioPlayer audioPlayer,
            Sound sound,
            SettingsPopupView settingsPopupView)
        {
            this.audioPlayer = audioPlayer != null ? audioPlayer : throw new ArgumentNullException(nameof(audioPlayer));
            this.sound = sound ?? throw new ArgumentNullException(nameof(sound));
            this.settingsPopupView = settingsPopupView != null ? settingsPopupView : throw new ArgumentNullException(nameof(settingsPopupView));
        }

        public void Initialize()
        {
            sound.VolumeChanged += OnVolumeChanged;
            sound.MusicChanged += OnMusicChanged;
            sound.TrapSoundChanged += OnTrapSoundChanged;
            sound.Loaded += SyncAudioAndViewWithSoundSettings;

            SyncAudioAndViewWithSoundSettings();
        }

        public void Dispose()
        {
            sound.VolumeChanged -= OnVolumeChanged;
            sound.MusicChanged -= OnMusicChanged;
            sound.TrapSoundChanged -= OnTrapSoundChanged;
            sound.Loaded -= SyncAudioAndViewWithSoundSettings;
        }

        private void OnVolumeChanged(float volumeValue)
        {
            audioPlayer.ChangeVolume(volumeValue);
        }

        private void OnMusicChanged(bool isOn)
        {
            audioPlayer.ChangeMusicOn(isOn);
        }

        private void OnTrapSoundChanged(bool isOn)
        {
            audioPlayer.ChangeTrapSoundOn(isOn);
        }

        private void SyncAudioAndViewWithSoundSettings()
        {
            audioPlayer.ChangeVolume(sound.Volume);
            audioPlayer.ChangeMusicOn(sound.IsMusicOn);
            audioPlayer.ChangeTrapSoundOn(sound.IsTrapSoundOn);

            settingsPopupView.SetSliderValue(sound.Volume);
            settingsPopupView.SetMusicSwitchOn(sound.IsMusicOn);
            settingsPopupView.SetTrapSoundSwitchOn(sound.IsTrapSoundOn);
        }
    }
}
