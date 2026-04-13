using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace TrapMaster
{
    public class UpgradeButtonsView : MonoBehaviour
    {
        [Header("Add Trap Button")]
        [SerializeField] private Button addTrapButton;
        [SerializeField] private Image addTrapImage;
        [SerializeField] private TextMeshProUGUI levelAddTrapText;
        [SerializeField] private TextMeshProUGUI priceAddTrapText;
        [SerializeField] private TextMeshProUGUI trapNameText;

        [Header("Add Monsters Button")]
        [SerializeField] private Button addMonstersButton;
        [SerializeField] private Image addMonstersImage;
        [SerializeField] private TextMeshProUGUI levelAddMonstersText;
        [SerializeField] private TextMeshProUGUI priceAddMonstersText;

        [Header("Add Speed Button")]
        [SerializeField] private Button addSpeedButton;
        [SerializeField] private Image addSpeedImage;
        [SerializeField] private TextMeshProUGUI levelAddSpeedText;
        [SerializeField] private TextMeshProUGUI priceAddSpeedText;

        [Header("Settings")]
        [SerializeField] private UpgradeUIColorsSetting colorsSetting;

        [Header("Other")]
        [SerializeField] private Button speedUpButton;

        public event Action AddTrapButtonClicked;
        public event Action AddMonstersButtonClicked;
        public event Action AddSpeedButtonClicked;
        public event Action SpeedUpButtonClicked;

        private string MaxText => YG2.envir.language == "ru" ? "МАКС" : "MAX";

        private void Awake()
        {
            ValidateReferences();
        }

        private void OnEnable()
        {
            addTrapButton.onClick.AddListener(HandleAddTrapClicked);
            addMonstersButton.onClick.AddListener(HandleAddMonstersClicked);
            addSpeedButton.onClick.AddListener(HandleAddSpeedClicked);
            speedUpButton.onClick.AddListener(HandleSpeedUpClicked);
        }

        private void OnDisable()
        {
            addTrapButton.onClick.RemoveListener(HandleAddTrapClicked);
            addMonstersButton.onClick.RemoveListener(HandleAddMonstersClicked);
            addSpeedButton.onClick.RemoveListener(HandleAddSpeedClicked);
            speedUpButton.onClick.RemoveListener(HandleSpeedUpClicked);
        }

        public void SetAddTrapLevelText(string levelText)
        {
            levelAddTrapText.text = levelText;
        }

        public void SetAddTrapPriceText(string priceText)
        {
            priceAddTrapText.text = priceText;
        }

        public void SetTrapNameText(string name)
        {
            trapNameText.text = name;
        }

        public void SetAddTrapButtonActive(bool isActive)
        {
            SetButtonState(addTrapButton, addTrapImage, isActive, colorsSetting.addTrapColor);
        }

        public void SetAddTrapMaxLevel()
        {
            SetMaxLevelState(addTrapButton, addTrapImage, priceAddTrapText);
        }

        public void SetAddMonstersLevelText(string levelText)
        {
            levelAddMonstersText.text = levelText;
        }

        public void SetAddMonstersPriceText(string priceText)
        {
            priceAddMonstersText.text = priceText;
        }

        public void SetAddMonstersButtonActive(bool isActive)
        {
            SetButtonState(addMonstersButton, addMonstersImage, isActive, colorsSetting.addMonstersColor);
        }

        public void SetAddMonstersMaxLevel()
        {
            SetMaxLevelState(addMonstersButton, addMonstersImage, priceAddMonstersText);
        }

        public void SetAddSpeedLevelText(string levelText)
        {
            levelAddSpeedText.text = levelText;
        }

        public void SetAddSpeedPriceText(string priceText)
        {
            priceAddSpeedText.text = priceText;
        }

        public void SetAddSpeedButtonActive(bool isActive)
        {
            SetButtonState(addSpeedButton, addSpeedImage, isActive, colorsSetting.addSpeedColor);
        }

        public void SetAddSpeedMaxLevel()
        {
            SetMaxLevelState(addSpeedButton, addSpeedImage, priceAddSpeedText);
        }

        private void HandleAddTrapClicked()
        {
            AddTrapButtonClicked?.Invoke();
        }

        private void HandleAddMonstersClicked()
        {
            AddMonstersButtonClicked?.Invoke();
        }

        private void HandleAddSpeedClicked()
        {
            AddSpeedButtonClicked?.Invoke();
        }

        private void HandleSpeedUpClicked()
        {
            SpeedUpButtonClicked?.Invoke();
        }

        private void SetButtonState(Button button, Image image, bool isActive, Color activeColor)
        {
            button.interactable = isActive;
            image.color = isActive ? activeColor : colorsSetting.inactiveColor;
        }

        private void SetMaxLevelState(Button button, Image image, TextMeshProUGUI priceText)
        {
            button.interactable = false;
            image.color = colorsSetting.inactiveColor;
            priceText.text = MaxText;
        }

        private void ValidateReferences()
        {
            LogIfMissing(addTrapButton, nameof(addTrapButton));
            LogIfMissing(addTrapImage, nameof(addTrapImage));
            LogIfMissing(trapNameText, nameof(trapNameText));
            LogIfMissing(levelAddTrapText, nameof(levelAddTrapText));
            LogIfMissing(priceAddTrapText, nameof(priceAddTrapText));

            LogIfMissing(addMonstersButton, nameof(addMonstersButton));
            LogIfMissing(addMonstersImage, nameof(addMonstersImage));
            LogIfMissing(levelAddMonstersText, nameof(levelAddMonstersText));
            LogIfMissing(priceAddMonstersText, nameof(priceAddMonstersText));

            LogIfMissing(addSpeedButton, nameof(addSpeedButton));
            LogIfMissing(addSpeedImage, nameof(addSpeedImage));
            LogIfMissing(levelAddSpeedText, nameof(levelAddSpeedText));
            LogIfMissing(priceAddSpeedText, nameof(priceAddSpeedText));

            LogIfMissing(colorsSetting, nameof(colorsSetting));
            LogIfMissing(speedUpButton, nameof(speedUpButton));
        }

        private void LogIfMissing(UnityEngine.Object target, string fieldName)
        {
            if (target == null)
                Debug.LogError($"{fieldName} is not assigned", this);
        }
    }
}