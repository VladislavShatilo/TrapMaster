using UnityEngine;

namespace TrapMaster
{
    [CreateAssetMenu(fileName = "SplashSettings", menuName = "Scriptable Objects/SplashSettings")]
    public class SplashSettings : ScriptableObject
    {
        [Header("Particles Settings")]
        public GameObject particleSplashObject;
        public float particleSplashPositionY = -0.5f;
        public int initialPoolSize = 8;
        public int maxPoolSize = 24;
    }

}