using System;
using Zenject;

namespace TrapMaster
{
    public class UpgradeAvailabilityPresenter : IInitializable, IDisposable
    {
        private UpgradeButtonsView upgradeButtonsView;
        private Wallet wallet;
        private Trap trap;
        private Monsters monsters;
        private Speed speed;
        private Stage stage;
        private StageCatalog stageCatalog;

        [Inject]
        public void Construct(
            UpgradeButtonsView upgradeButtonsView,
            Wallet wallet,
            Trap trap,
            Monsters monsters,
            Speed speed,
            Stage stage,
            StageCatalog stageCatalog)
        {
            this.upgradeButtonsView = upgradeButtonsView ?? throw new ArgumentNullException(nameof(upgradeButtonsView));
            this.wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));
            this.trap = trap ?? throw new ArgumentNullException(nameof(trap));
            this.monsters = monsters ?? throw new ArgumentNullException(nameof(monsters));
            this.speed = speed ?? throw new ArgumentNullException(nameof(speed));
            this.stage = stage ?? throw new ArgumentNullException(nameof(stage));
            this.stageCatalog = stageCatalog ?? throw new ArgumentNullException(nameof(stageCatalog));
        }

        public void Initialize()
        {
            wallet.Changed += OnWalletChanged;
            stage.Changed += OnStageChanged;

            trap.PriceChanged += OnTrapStateChanged;
            trap.LevelChanged += OnTrapStateChanged;

            monsters.PriceChanged += OnMonstersStateChanged;
            monsters.LevelChanged += OnMonstersStateChanged;

            speed.PriceChanged += OnSpeedStateChanged;
            speed.LevelChanged += OnSpeedStateChanged;

            Refresh();
        }

        public void Dispose()
        {
            wallet.Changed -= OnWalletChanged;
            stage.Changed -= OnStageChanged;

            trap.PriceChanged -= OnTrapStateChanged;
            trap.LevelChanged -= OnTrapStateChanged;

            monsters.PriceChanged -= OnMonstersStateChanged;
            monsters.LevelChanged -= OnMonstersStateChanged;

            speed.PriceChanged -= OnSpeedStateChanged;
            speed.LevelChanged -= OnSpeedStateChanged;
        }

        private void OnWalletChanged(int _) => Refresh();
        private void OnStageChanged(int _) => Refresh();

        private void OnTrapStateChanged(int _) => RefreshTrap();
        private void OnMonstersStateChanged(int _) => RefreshMonsters();
        private void OnSpeedStateChanged(int _) => RefreshSpeed();

        private void Refresh()
        {
            RefreshTrap();
            RefreshMonsters();
            RefreshSpeed();
        }

        private void RefreshTrap()
        {
            var stageSetting = stageCatalog.GetByNumber(stage.Number);

            RefreshUpgrade(
                level: trap.Level,
                price: trap.Price,
                prices: stageSetting?.addTrapPrices,
                setActive: upgradeButtonsView.SetAddTrapButtonActive,
                setMaxLevel: upgradeButtonsView.SetAddTrapMaxLevel);
        }

        private void RefreshMonsters()
        {
            var stageSetting = stageCatalog.GetByNumber(stage.Number);

            RefreshUpgrade(
                level: monsters.Level,
                price: monsters.Price,
                prices: stageSetting?.addMonsterPrices,
                setActive: upgradeButtonsView.SetAddMonstersButtonActive,
                setMaxLevel: upgradeButtonsView.SetAddMonstersMaxLevel);
        }

        private void RefreshSpeed()
        {
            var stageSetting = stageCatalog.GetByNumber(stage.Number);

            RefreshUpgrade(
                level: speed.Level,
                price: speed.Price,
                prices: stageSetting?.addSpeedPrices,
                setActive: upgradeButtonsView.SetAddSpeedButtonActive,
                setMaxLevel: upgradeButtonsView.SetAddSpeedMaxLevel);
        }
        public void RefreashAll()
        {
            RefreshTrap();
            RefreshMonsters();
            RefreshSpeed();
        }
        private void RefreshUpgrade(
            int level,
            int price,
            int[] prices,
            Action<bool> setActive,
            Action setMaxLevel)
        {
            if (prices == null)
            {
                setActive(false);
                return;
            }

            if (level >= prices.Length)
            {
                setMaxLevel();
                return;
            }

            setActive(wallet.Coins >= price);
        }
    }
}