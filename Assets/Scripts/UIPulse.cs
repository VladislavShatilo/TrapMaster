using UnityEngine;
using DG.Tweening;

namespace TrapMaster
{
    public class UIPulse : MonoBehaviour
    {
        [SerializeField] private float duration = 0.5f;
        [SerializeField] private float scaleMultiplier = 1.2f;

        private Tween pulseTween;

        private void OnEnable()
        {
            PlayPulse();
        }

        private void OnDisable()
        {
            KillTween();
        }

        private void PlayPulse()
        {
            KillTween(); 

            pulseTween = transform
                .DOScale(Vector3.one * scaleMultiplier, duration)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }

        private void KillTween()
        {
            if (pulseTween == null)
                return;

            pulseTween.Kill();
            pulseTween = null;

            transform.localScale = Vector3.one;
        }
    }
}