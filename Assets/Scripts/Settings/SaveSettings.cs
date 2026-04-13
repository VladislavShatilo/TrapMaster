using UnityEngine;

namespace TrapMaster
{
    [CreateAssetMenu(fileName = "SaveSettings", menuName = "Scriptable Objects/SaveSettings")]
    public class SaveSettings : ScriptableObject
    {
        public int saveInterval = 2; 

    }

}