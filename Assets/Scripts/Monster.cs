using System;
using UnityEngine;
using Zenject;

namespace TrapMaster
{
    public class Monster : MonoBehaviour
    {
        private const float MoveSpeed = 5f;

        private EventBus eventBus;
        private bool isDead;

        public bool IsX3 { get; private set; }

        public event Action<Monster> Despawned;

        [Inject]
        public void Construct(EventBus eventBus)
        {
            this.eventBus = eventBus;
        }

        public void Setup(bool isTripleReward)
        {
            isDead = false;
            IsX3 = isTripleReward;
        }

        public void Die()
        {
            if (!TryMarkAsDead())
                return;

            eventBus.Publish(new MonsterDie(transform.position, IsX3));
            Release();
        }

        public void DespawnSilently()
        {
            if (!TryMarkAsDead())
                return;

            Release();
        }

        private void Update()
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        }

        private bool TryMarkAsDead()
        {
            if (isDead)
                return false;

            isDead = true;
            return true;
        }

        private void Release()
        {
            Despawned?.Invoke(this);
            Destroy(gameObject);
        }
    }
}