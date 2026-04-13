using System;
using UnityEngine;
using Zenject;

namespace TrapMaster
{
    public class MonsterFactory : IMonsterFactory
    {
        private DiContainer container;
        private SpawnSetting spawnSetting;

        [Inject]
        public void Construct(DiContainer container, SpawnSetting spawnSetting)
        {
            this.container = container ?? throw new ArgumentNullException(nameof(container));
            this.spawnSetting = spawnSetting ?? throw new ArgumentNullException(nameof(spawnSetting));
        }

        public Monster Create(Vector3 position, Quaternion rotation)
        {
            bool isTripleReward = ShouldSpawnTripleRewardMonster();
            Monster prefab = isTripleReward && spawnSetting.monsterX3Prefab != null
                ? spawnSetting.monsterX3Prefab
                : spawnSetting.monsterPrefab;

            Monster monster = container.InstantiatePrefabForComponent<Monster>(
                prefab,
                position,
                rotation,
                null);

            monster.Setup(isTripleReward);
            return monster;
        }

        public void DestroyMonster(Monster monster)
        {
            if (monster == null)
                throw new ArgumentNullException(nameof(monster));

            UnityEngine.Object.Destroy(monster.gameObject);
        }

        private bool ShouldSpawnTripleRewardMonster()
        {
            return spawnSetting.monsterX3Prefab != null &&
                   UnityEngine.Random.value <= spawnSetting.blueMonsterChance;
        }
    }
}