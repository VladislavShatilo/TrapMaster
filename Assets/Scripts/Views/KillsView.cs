using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TrapMaster
{
    public class KillsView : MonoBehaviour, IUIKillsView
    {
        [Header("Top UI")]
        [SerializeField] private TextMeshProUGUI coinsAmountText;
        [SerializeField] private TextMeshProUGUI killsAmountText;
        [SerializeField] private Slider killsSlider;

        private void Awake()
        {
            ValidateReferences();
        }

        public void SetCoinsAmountText(int coins)
        {
            coinsAmountText.text = NumberFormatter.Format(coins);
        }

        public void SetKillsSliderText(string killsText)
        {
            killsAmountText.text = killsText;
        }

        public void SetKillsSliderValue(float value)
        {
            killsSlider.value = value;
        }

        public void SetKillsSliderMaxValue(int maxValue)
        {
            killsSlider.maxValue = maxValue;
        }

        private void ValidateReferences()
        {
            LogIfMissing(coinsAmountText, nameof(coinsAmountText));
            LogIfMissing(killsAmountText, nameof(killsAmountText));
            LogIfMissing(killsSlider, nameof(killsSlider));
        }

        private void LogIfMissing(Object target, string fieldName)
        {
            if (target == null)
                Debug.LogError($"{fieldName} is not assigned", this);
        }
    }
}