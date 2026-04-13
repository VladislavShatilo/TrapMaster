using System;
using UnityEngine;
using Zenject;

namespace TrapMaster
{
    public class Mace : MonoBehaviour, ITrap
    {
        [SerializeField] private GameObject maceGo;

        private Speed speed;
        private float direction;

        [Inject]
        public void Construct(Speed speed)
        {
            this.speed = speed ?? throw new ArgumentNullException(nameof(speed));
        }

        private void Awake()
        {
            Debug.Assert(maceGo != null, $"{nameof(maceGo)} is not assigned");
        }

        private void Start()
        {
            direction = transform.position.x < 0 ? -1f : 1f;
        }

        private void Update()
        {
            float rotationY = direction * speed.Value * Time.deltaTime;
            maceGo.transform.Rotate(0f, rotationY, 0f);
        }
    }
}