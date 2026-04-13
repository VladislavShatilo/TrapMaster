using UnityEngine;

namespace TrapMaster
{
    public class DestroyOtherMonsters : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Monster monster))
            {
                monster.DespawnSilently();
            }
            else
            {
                Destroy(other.gameObject);
            }
        }
    }
}