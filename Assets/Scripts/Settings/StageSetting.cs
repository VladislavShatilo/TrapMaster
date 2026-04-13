using UnityEngine;

namespace TrapMaster
{
    [CreateAssetMenu(fileName = "StageSetting", menuName = "Scriptable Objects/StageSetting")]
    public class StageSetting : ScriptableObject
    {
        public int stageNumber;
        public TrapType trapType;
        public int targetKills;

        [Header("Trap Setup")]
        public GameObject trapPrefab;
        public Vector3[] trapPositions;

        [Header("Prices")]
        public int[] addTrapPrices;
        public int[] addSpeedPrices;
        public int[] addMonsterPrices = new int[]{ 0,6,9,  15,21, 27,36,45, 54,66,81, 99, 117,138,162, 192,222,261, 300,348, 396, 456,516,
        588, 660,744, 828, 924,1020,1128,1236,1356, 1476, 1608,1740,1884, 2028, 2184, 2340,2508};

        [Header("Trap Runtime Params")]
        public float baseSpeed;

        [Header("Upgrade Multipliers")]
        public float speedMultiplier = 1.1f;
        
    }

}