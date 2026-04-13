using UnityEngine;

namespace TrapMaster
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private float minX = -2.5f;
        [SerializeField] private float maxX = 2.5f;

        public Vector3 Position
        {
            get
            {
                float randomX = Random.Range(minX, maxX);
                var basePosition = transform.position;

                return new Vector3(randomX, basePosition.y, basePosition.z);
            }
        }

        public Quaternion Rotation => transform.rotation;
    }
}