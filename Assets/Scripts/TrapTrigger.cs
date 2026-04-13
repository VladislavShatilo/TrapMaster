using UnityEngine;

namespace TrapMaster
{
    public class TrapTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Monster monster))
                return;

            monster.Die();
        }
    }
}