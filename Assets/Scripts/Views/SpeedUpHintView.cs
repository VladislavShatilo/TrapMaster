using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TrapMaster
{
    public class SpeedUpHintView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI hintText;
        [SerializeField] private Image fingerTapImage;

        private void Awake()
        {
            ValidateReferences();
            SetVisible(false);
        }

        public void Show() => SetVisible(true);

        public void Hide() => SetVisible(false);

        private void SetVisible(bool isVisible)
        {
            hintText.gameObject.SetActive(isVisible);
            fingerTapImage.gameObject.SetActive(isVisible);
        }

        private void ValidateReferences()
        {
            LogIfMissing(hintText, nameof(hintText));
            LogIfMissing(fingerTapImage, nameof(fingerTapImage));
        }

        private void LogIfMissing(Object target, string fieldName)
        {
            if (target == null)
                Debug.LogError($"{fieldName} is not assigned", this);
        }
    }
}