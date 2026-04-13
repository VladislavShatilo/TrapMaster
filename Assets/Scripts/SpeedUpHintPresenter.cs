using System;
using UnityEngine;
using Zenject;

namespace TrapMaster
{
    public class SpeedUpHintPresenter : IInitializable, ITickable
    {
        private const float ShowDelay = 30f;
        private const float ShowDuration = 10f;

        private SpeedUpHintView hintView;
        private SessionTimer sessionTimer;

        private bool wasShown;
        private bool isShowing;
        private float showTimer;

        [Inject]
        public void Construct(
            SpeedUpHintView hintView,
            SessionTimer sessionTimer)
        {
            this.hintView = hintView ?? throw new ArgumentNullException(nameof(hintView));
            this.sessionTimer = sessionTimer ?? throw new ArgumentNullException(nameof(sessionTimer));
        }

        public void Initialize()
        {
            hintView.Hide();
        }

        public void Tick()
        {
            if (ShouldShowHint())
            {
                ShowHint();
                return;
            }

            if (isShowing)
            {
                UpdateHintTimer();
            }
        }

        private bool ShouldShowHint()
        {
            return !wasShown && sessionTimer.ElapsedTime >= ShowDelay;
        }

        private void UpdateHintTimer()
        {
            showTimer -= Time.deltaTime;

            if (showTimer <= 0f)
            {
                HideHint();
            }
        }

        private void ShowHint()
        {
            wasShown = true;
            isShowing = true;
            showTimer = ShowDuration;

            hintView.Show();
        }

        private void HideHint()
        {
            isShowing = false;
            hintView.Hide();
        }
    }
}