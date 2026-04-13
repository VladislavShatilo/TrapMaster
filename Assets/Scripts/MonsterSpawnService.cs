using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TrapMaster
{
    public class MonsterSpawnService : IInitializable
    {
        private SpawnSetting spawnSetting;
        private SpawnPoint spawnPoint;
        private Monsters monsters;
        private IMonsterFactory monsterFactory;

        private float spawnInterval;
        private float timer;

        private readonly List<Monster> monstersList = new();
        private readonly Queue<float> speedUpTapTimes = new();

        [Inject]
        public void Construct(
            SpawnSetting spawnSetting,
            IMonsterFactory monsterFactory,
            SpawnPoint spawnPoint,
            Monsters monsters)
        {
            this.spawnSetting = spawnSetting ?? throw new ArgumentNullException(nameof(spawnSetting));
            this.monsterFactory = monsterFactory ?? throw new ArgumentNullException(nameof(monsterFactory));
            this.spawnPoint = spawnPoint ?? throw new ArgumentNullException(nameof(spawnPoint));
            this.monsters = monsters ?? throw new ArgumentNullException(nameof(monsters));
        }

        public void Initialize()
        {
            UpdateSpawnInterval();
        }

        public void UpdateSpawnInterval()
        {
            int level = monsters.Level;
            spawnInterval = spawnSetting.startSpawnInterval *
                             (float)Math.Pow(spawnSetting.increaseTimeMultiplier, level);
        }

        public void RegisterSpeedUpTap()
        {
            float now = Time.unscaledTime;
            speedUpTapTimes.Enqueue(now);
            RemoveExpiredTaps(now);
        }

        public void Tick(float deltaTime)
        {
            float now = Time.unscaledTime;
            RemoveExpiredTaps(now);

            timer += deltaTime;

            float currentSpawnInterval = GetCurrentSpawnInterval();
            if (timer < currentSpawnInterval)
                return;

            timer -= currentSpawnInterval;
            SpawnMonster();
        }

        public void DestroyAllMonsters()
        {
            Monster[] spawnedMonsters = monstersList.ToArray();

            foreach (Monster monster in spawnedMonsters)
            {
                if (monster == null)
                    continue;

                monster.Despawned -= OnMonsterDespawned;
                monsterFactory.DestroyMonster(monster);
            }

            monstersList.Clear();
        }

        private float GetCurrentSpawnInterval()
        {
            bool isFastSpawnEnabled =
                speedUpTapTimes.Count >= spawnSetting.requiredTapsPerSecond;

            return isFastSpawnEnabled
                ? spawnInterval / spawnSetting.fastSpawnMultiplier
                : spawnInterval;
        }

        private void RemoveExpiredTaps(float now)
        {
            while (speedUpTapTimes.Count > 0 &&
                   now - speedUpTapTimes.Peek() > spawnSetting.tapWindow)
            {
                speedUpTapTimes.Dequeue();
            }
        }

        private void SpawnMonster()
        {
            Monster monster = monsterFactory.Create(
                spawnPoint.Position,
                spawnPoint.Rotation);

            monster.Despawned += OnMonsterDespawned;
            monstersList.Add(monster);
        }

        private void OnMonsterDespawned(Monster monster)
        {
            if (monster == null)
                return;

            monster.Despawned -= OnMonsterDespawned;
            monstersList.Remove(monster);
        }
    }
}