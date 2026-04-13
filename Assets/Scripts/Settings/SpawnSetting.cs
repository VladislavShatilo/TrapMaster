using UnityEngine;

namespace TrapMaster
{
    [CreateAssetMenu(fileName = "SpawnSetting", menuName = "Scriptable Objects/SpawnSetting")]
    public class SpawnSetting : ScriptableObject
    {
        public float startSpawnInterval = 1f;
        public float increaseTimeMultiplier = 0.95f;

        public float spawnInterval = 1f;
        public float blueMonsterChance = 0.15f;

        public Monster monsterX3Prefab;
        public Monster monsterPrefab;

        public int requiredTapsPerSecond = 4;
        public float tapWindow = 1f;
        public float fastSpawnMultiplier = 2f;

    }

}