using System;
using UnityEngine;
using Zenject;

namespace TrapMaster
{
    public class Blade : MonoBehaviour, ITrap
    {
        [SerializeField] private Transform bladeTransform;

        [Header("Blade Settings")]
        [SerializeField] private float leftX = -5f;
        [SerializeField] private float rightX = 5f;
        [SerializeField] private Vector3 rotationSpeed = new(0f, 100f, 0f);

        private float targetX;
        private Speed speed;

        [Inject]
        public void Construct(Speed speed)
        {
            this.speed = speed ?? throw new ArgumentNullException(nameof(speed));
        }

        private void Awake()
        {
            if (bladeTransform == null)
            {
                Debug.LogError($"{nameof(bladeTransform)} is not assigned", this);
                enabled = false;
                
            }
        }

        private void Start()
        {
            float randomX = UnityEngine.Random.Range(leftX, rightX);

            var position = bladeTransform.position;
            position.x = randomX;
            bladeTransform.position = position;

            targetX = UnityEngine.Random.value > 0.5f ? leftX : rightX;
        }

        private void Update()
        {
            Move();
            Rotate();
        }

        private void Move()
        {
            var position = bladeTransform.position;

            position.x = Mathf.MoveTowards(
                position.x,
                targetX,
                speed.Value * Time.deltaTime);

            bladeTransform.position = position;

            if (Mathf.Approximately(position.x, targetX))
            {
                targetX = targetX == leftX ? rightX : leftX;
            }
        }

        private void Rotate()
        {
            bladeTransform.Rotate(rotationSpeed * Time.deltaTime);
        }
    }
}