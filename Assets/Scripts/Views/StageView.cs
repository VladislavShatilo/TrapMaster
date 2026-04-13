using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace TrapMaster
{
    public class StageView : MonoBehaviour
    {
        [Header("Animation")]
        [SerializeField] private RectTransform panel;
        [SerializeField] private Image backgroundImage;
        [SerializeField] private float animationTime = 0.5f;
        [SerializeField] private float shownY = 150f;

        [Header("UI")]
        [SerializeField] private TextMeshProUGUI stageText;
        [SerializeField] private Button nextStageButton;

        public event Action NextStageClicked;

        private float hiddenY;

        private void Awake()
        {
            ValidateReferences();

            hiddenY = -Screen.height;
            panel.anchoredPosition = new Vector2(0f, hiddenY);

            backgroundImage.gameObject.SetActive(false);
            panel.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            nextStageButton.onClick.AddListener(HandleNextStageClicked);
        }

        private void OnDisable()
        {
            nextStageButton.onClick.RemoveListener(HandleNextStageClicked);
            panel.DOKill();
        }

        public void Show()
        {
            panel.DOKill();

            panel.gameObject.SetActive(true);
            backgroundImage.gameObject.SetActive(true);

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

        public void SetStageText(int stageNumber)
        {
            string stageLabel = YG2.envir.language == "ru" ? "СТАДИЯ" : "STAGE";
            stageText.text = $"{stageLabel} {stageNumber}";
        }

        private void HandleNextStageClicked()
        {
            NextStageClicked?.Invoke();
        }

        private void ValidateReferences()
        {
            LogIfMissing(panel, nameof(panel));
            LogIfMissing(backgroundImage, nameof(backgroundImage));
            LogIfMissing(stageText, nameof(stageText));
            LogIfMissing(nextStageButton, nameof(nextStageButton));
        }

        private void LogIfMissing(UnityEngine.Object target, string fieldName)
        {
            if (target == null)
                Debug.LogError($"{fieldName} is not assigned", this);
        }
    }
}