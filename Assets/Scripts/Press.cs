using System;
using UnityEngine;
using Zenject;

namespace TrapMaster
{
    public class Press : MonoBehaviour, ITrap
    {
        private const float ReachThreshold = 0.001f;

        [Header("Press Parts")]
        [SerializeField] private Transform mainPressLeft;
        [SerializeField] private Transform mainPressRight;

        [Header("Animation Settings")]
        [SerializeField] private float moveDistance = 1.2f;
        [SerializeField] private float moveSpeed = 4f;
        [SerializeField] private float holdClosedTime = 0.3f;

        private Speed speed;

        private Vector3 leftOpenPosition;
        private Vector3 rightOpenPosition;
        private Vector3 leftClosedPosition;
        private Vector3 rightClosedPosition;

        private float waitTimer;
        private bool isClosing = true;
        private bool isWaiting;

        [Inject]
        public void Construct(Speed speed)
        {
            this.speed = speed ?? throw new ArgumentNullException(nameof(speed));
        }

        private void Awake()
        {
            if (mainPressLeft == null)
                throw new NullReferenceException(nameof(mainPressLeft));

            if (mainPressRight == null)
                throw new NullReferenceException(nameof(mainPressRight));

            leftOpenPosition = mainPressLeft.localPosition;
            rightOpenPosition = mainPressRight.localPosition;

            var offset = Vector3.forward * moveDistance;
            leftClosedPosition = leftOpenPosition + offset;
            rightClosedPosition = rightOpenPosition + offset;
        }

        private void Update()
        {
            if (isWaiting)
            {
                UpdateWaitTimer();
                return;
            }

            AnimateMovement();
        }

        private void UpdateWaitTimer()
        {
            waitTimer -= Time.deltaTime;

            if (waitTimer > 0f)
                return;

            isWaiting = false;
            isClosing = !isClosing;
        }

        private void AnimateMovement()
        {
            var targetLeft = isClosing ? leftClosedPosition : leftOpenPosition;
            var targetRight = isClosing ? rightClosedPosition : rightOpenPosition;

            mainPressLeft.localPosition = MoveTowards(mainPressLeft.localPosition, targetLeft);
            mainPressRight.localPosition = MoveTowards(mainPressRight.localPosition, targetRight);

            if (!HasReachedTarget(mainPressLeft.localPosition, targetLeft) ||
                !HasReachedTarget(mainPressRight.localPosition, targetRight))
            {
                return;
            }

            mainPressLeft.localPosition = targetLeft;
            mainPressRight.localPosition = targetRight;

            isWaiting = true;
            waitTimer = isClosing ? holdClosedTime : speed.Value;
        }

        private Vector3 MoveTowards(Vector3 current, Vector3 target)
        {
            return Vector3.MoveTowards(current, target, moveSpeed * Time.deltaTime);
        }

        private static bool HasReachedTarget(Vector3 current, Vector3 target)
        {
            return Vector3.Distance(current, target) < ReachThreshold;
        }
    }
}