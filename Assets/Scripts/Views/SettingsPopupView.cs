using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UISwitch = UISwitcher.UISwitcher;

namespace TrapMaster
{
    public class SettingsPopupView : MonoBehaviour, ISettingsPopupView
    {
        [Header("Animation")]
        [SerializeField] private RectTransform panel;
        [SerializeField] private Image backgroundImage;
        [SerializeField] private float animationTime = 0.5f;
        [SerializeField] private float shownY = 150f;

        [Header("UI")]
        [SerializeField] private Button closeButton;
        [SerializeField] private Slider volumeSlider;
        [SerializeField] private UISwitch musicSwitcher;
        [SerializeField] private UISwitch trapAudioSwitcher;

        public event Action CloseClicked;
        public event Action<bool> MusicChanged;
        public event Action<bool> TrapAudioChanged;
        public event Action<float> VolumeChanged;

        private float hiddenY;

        private void Awake()
        {
            ValidateReferences();

            hiddenY = -Screen.height;
            panel.anchoredPosition = new Vector2(0f, hiddenY);

            panel.gameObject.SetActive(false);
            backgroundImage.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            closeButton.onClick.AddListener(HandleCloseClicked);
            musicSwitcher.onValueChanged.AddListener(HandleMusicChanged);
            trapAudioSwitcher.onValueChanged.AddListener(HandleTrapAudioChanged);
            volumeSlider.onValueChanged.AddListener(HandleVolumeChanged);
        }

        private void OnDisable()
        {
            closeButton.onClick.RemoveListener(HandleCloseClicked);
            musicSwitcher.onValueChanged.RemoveListener(HandleMusicChanged);
            trapAudioSwitcher.onValueChanged.RemoveListener(HandleTrapAudioChanged);
            volumeSlider.onValueChanged.RemoveListener(HandleVolumeChanged);

            panel.DOKill();
        }

        public void Show()
        {
            panel.DOKill();

            panel.gameObject.SetActive(true);
            backgroundImage.gameObject.SetActive(true);
            panel.anchoredPosition = new Vector2(panel.anchoredPosition.x, hiddenY);

            panel
                .DOAnchorPosY(shownY, animationTime)
                .SetEase(Ease.OutCubic);
        }

        public void Hide()
        {
            panel.DOKill();

            panel
                .DOAnchorPosY(hiddenY, animationTime)
                .SetEase(Ease.InCubic)
                .OnComplete(() =>
                {
                    panel.gameObject.SetActive(false);
                    backgroundImage.gameObject.SetActive(false);
                });
        }

        public void SetSliderValue(float volumeValue)
        {
            volumeSlider.SetValueWithoutNotify(volumeValue);
        }

        public void SetMusicSwitchOn(bool isOn)
        {
            musicSwitcher.SetWithoutNotify(isOn);
        }

        public void SetTrapSoundSwitchOn(bool isOn)
        {
            trapAudioSwitcher.SetWithoutNotify(isOn);
        }

        private void HandleCloseClicked()
        {
            CloseClicked?.Invoke();
        }

        private void HandleMusicChanged(bool isOn)
        {
            MusicChanged?.Invoke(isOn);
        }

        private void HandleTrapAudioChanged(bool isOn)
        {
            TrapAudioChanged?.Invoke(isOn);
        }

        private void HandleVolumeChanged(float value)
        {
            VolumeChanged?.Invoke(value);
        }

        private void ValidateReferences()
        {
            LogIfMissing(panel, nameof(panel));
            LogIfMissing(backgroundImage, nameof(backgroundImage));
            LogIfMissing(closeButton, nameof(closeButton));
            LogIfMissing(volumeSlider, nameof(volumeSlider));
            LogIfMissing(musicSwitcher, nameof(musicSwitcher));
            LogIfMissing(trapAudioSwitcher, nameof(trapAudioSwitcher));
        }

        private void LogIfMissing(UnityEngine.Object target, string fieldName)
        {
            if (target == null)
                Debug.LogError($"{fieldName} is not assigned", this);
        }
    }
}