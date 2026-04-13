using UnityEngine;

namespace TrapMaster
{
    [CreateAssetMenu(fileName = "UpgradeUIColorsSetting", menuName = "Scriptable Objects/UpgradeUIColorsSetting")]
    public class UpgradeUIColorsSetting : ScriptableObject
    {
        public Color addTrapColor = new Color(0f, 158f / 255f, 255f / 255f, 1f);
        public Color addMonstersColor = new Color(0f, 238f / 255f, 14f / 255f, 1f);
        public Color addSpeedColor = new Color(153f / 255f, 0f / 255f, 255f / 255f, 1f);
        public Color inactiveColor = new Color(148f / 255f, 148f / 255f, 148f / 255f, 1f);
    }

}