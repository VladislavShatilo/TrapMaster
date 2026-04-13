using UnityEngine;

namespace TrapMaster
{
    public interface ITrapFactory 
    {
        ITrap AddTrap(Vector3 position, Quaternion rotation);
        void DestroyTrap(ITrap trap);
    }

}