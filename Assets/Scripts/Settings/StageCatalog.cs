using UnityEngine;

namespace TrapMaster
{
    [CreateAssetMenu(fileName = "StageCatalog", menuName = "Scriptable Objects/StageCatalog")]
    public class StageCatalog : ScriptableObject
    {
        public StageSetting[] stages;

        public StageSetting GetByNumber(int stageNumber)
        {
            foreach (var stage in stages)
            {
                if (stage.stageNumber == stageNumber)
                    return stage;
            }

            return null;
        }
    }

}