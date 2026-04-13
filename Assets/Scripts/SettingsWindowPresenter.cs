using System;
using Zenject;

namespace TrapMaster
{
    public class SettingsWindowPresenter : IInitializable, IDisposable
    {
        private SettingsButtonView settingsButtonView;
        private SettingsPopupView settingsPopupView;
        private Sound sound;

        [Inject]
        public void Construct(
            SettingsButtonView settingsButtonView,
            SettingsPopupView settingsPopupView,
            Sound sound)
        {
            this.settingsButtonView = settingsButtonView ?? throw new ArgumentNullException(nameof(settingsButtonView));
            this.settingsPopupView = settingsPopupView ?? throw new ArgumentNullException(nameof(settingsPopupView));
            this.sound = sound ?? throw new ArgumentNullException(nameof(sound));
        }

        public void Initialize()
        {
            Subscribe();
        }

        public void Dispose()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            settingsButtonView.Clicked += OnSettingsOpened;
            settingsPopupView.CloseClicked += OnSettingsClosed;
            settingsPopupView.MusicChanged += OnMusicChanged;
            settingsPopupView.TrapAudioChanged += OnTrapSoundChanged;
            settingsPopupView.VolumeChanged += OnVolumeChanged;
        }

        private void Unsubscribe()
        {
            settingsButtonView.Clicked -= OnSettingsOpened;
            settingsPopupView.CloseClicked -= OnSettingsClosed;
            settingsPopupView.MusicChanged -= OnMusicChanged;
            settingsPopupView.TrapAudioChanged -= OnTrapSoundChanged;
            settingsPopupView.VolumeChanged -= OnVolumeChanged;
        }

        private void OnSettingsOpened()
        {
            settingsPopupView.Show();
        }

        private void OnSettingsClosed()
        {
            settingsPopupView.Hide();
        }

        private void OnMusicChanged(bool isOn)
        {
            sound.SetMusicOn(isOn);
        }

        private void OnTrapSoundChanged(bool isOn)
        {
            sound.SetTrapSoundOn(isOn);
        }

        private void OnVolumeChanged(float volume)
        {
            sound.SetVolume(volume);
        }
    }
}