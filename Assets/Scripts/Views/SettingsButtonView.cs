using System;
using UnityEngine;
using UnityEngine.UI;

namespace TrapMaster
{
    public class SettingsButtonView : MonoBehaviour
    {
        [SerializeField] private Button openButton;

        public event Action Clicked;

        private void Awake()
        {
            if (openButton == null)
            {
                Debug.LogError($"{nameof(openButton)} is not assigned", this);
                enabled = false;
            }
        }

        private void OnEnable()
        {
            openButton.onClick.AddListener(HandleClick);
        }

        private void OnDisable()
        {
            openButton.onClick.RemoveListener(HandleClick);
        }

        private void HandleClick()
        {
            Clicked?.Invoke();
        }
    }
}