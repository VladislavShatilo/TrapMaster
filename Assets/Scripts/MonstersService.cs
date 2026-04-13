using System;
using Zenject;

namespace TrapMaster
{
    public class MonstersService
    {
        private Monsters monsters;
        private Wallet wallet;
        private MonsterSpawnService monsterSpawnService;
        private StageCatalog stageCatalog;
        private Stage stage;

        [Inject]
        public void Construct(
            Monsters monsters,
            Wallet wallet,
            MonsterSpawnService monsterSpawnService,
            StageCatalog stageCatalog,
            Stage stage)
        {
            this.monsters = monsters ?? throw new ArgumentNullException(nameof(monsters));
            this.wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));
            this.monsterSpawnService = monsterSpawnService ?? throw new ArgumentNullException(nameof(monsterSpawnService));
            this.stageCatalog = stageCatalog ?? throw new ArgumentNullException(nameof(stageCatalog));
            this.stage = stage ?? throw new ArgumentNullException(nameof(stage));
        }

        public bool TryAddMonsters()
        {
            var stageConfig = stageCatalog.GetByNumber(stage.Number);
            var addMonsterPrices = stageConfig.addMonsterPrices;

            if (monsters.Level >= addMonsterPrices.Length)
                return false;

            var nextLevelPrice = monsters.Price;

            if (wallet.Coins < nextLevelPrice)
                return false;

            if (!wallet.TrySpendCoins(nextLevelPrice))
                return false;

            monsters.IncreaseLevel();

            if (monsters.Level < addMonsterPrices.Length)
            {
                monsters.SetPrice(addMonsterPrices[monsters.Level]);
            }

            monsterSpawnService.UpdateSpawnInterval();
            return true;
        }
    }
}
