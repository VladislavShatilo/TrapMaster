using UnityEngine;
using Zenject;

namespace TrapMaster
{
    public class MonsterSpawnTickable : ITickable
    {
        private MonsterSpawnService monsterSpawnService;

        [Inject]
        public void Construct(MonsterSpawnService monsterSpawnService)
        {
            this.monsterSpawnService = monsterSpawnService;
        }

        public void Tick()
        {
            monsterSpawnService.Tick(Time.deltaTime);
        }
    }
}