using UnityEngine;

namespace TrapMaster
{
    public interface IMonsterFactory 
    {
        Monster Create(Vector3 position, Quaternion rotation);
        void DestroyMonster(Monster monster);
    }

}