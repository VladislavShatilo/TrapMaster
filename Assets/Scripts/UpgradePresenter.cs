using System;
using Zenject;

namespace TrapMaster
{
    public class UpgradePresenter : IInitializable, IDisposable
    {
        private UpgradeButtonsView upgradeButtonsView;
        private TrapService trapService;
        private Trap trap;
        private Speed speed;
        private Monsters monsters;
        private MonstersService monstersService;
        private SpeedService speedService;
        private MonsterSpawnService monsterSpawnService;
        private Stage stage;
        private StageCatalog stageCatalog;

        [Inject]
        public void  Construct(
            UpgradeButtonsView upgradeButtonsView,
            TrapService trapService,
            Trap trap,
            MonstersService monstersService,
            Monsters monsters,
            Speed speed,
            SpeedService speedService,
            MonsterSpawnService monsterSpawnService,
            Stage stage,
            StageCatalog stageCatalog)
        {
            this.upgradeButtonsView = upgradeButtonsView ?? throw new ArgumentNullException(nameof(upgradeButtonsView));
            this.trapService = trapService ?? throw new ArgumentNullException(nameof(trapService));
            this.trap = trap ?? throw new ArgumentNullException(nameof(trap));
            this.monstersService = monstersService ?? throw new ArgumentNullException(nameof(monstersService));
            this.monsters = monsters ?? throw new ArgumentNullException(nameof(monsters));
            this.speed = speed ?? throw new ArgumentNullException(nameof(speed));
            this.speedService = speedService ?? throw new ArgumentNullException(nameof(speedService));
            this.monsterSpawnService = monsterSpawnService ?? throw new ArgumentNullException(nameof(monsterSpawnService));
            this.stage = stage ?? throw new ArgumentNullException(nameof(stage));
            this.stageCatalog = stageCatalog ?? throw new ArgumentNullException(nameof(stageCatalog));
        }

        public void Initialize()
        {
            upgradeButtonsView.AddTrapButtonClicked += OnTrapClicked;
            upgradeButtonsView.AddMonstersButtonClicked += OnMonstersClicked;
            upgradeButtonsView.AddSpeedButtonClicked += OnSpeedClicked;
            upgradeButtonsView.SpeedUpButtonClicked += OnSpeedUpClicked;

            trap.PriceChanged += OnTrapPriceChanged;
            trap.LevelChanged += OnTrapLevelChanged;

            monsters.PriceChanged += OnMonstersPriceChanged;
            monsters.LevelChanged += OnMonstersLevelChanged;

            speed.PriceChanged += OnSpeedPriceChanged;
            speed.LevelChanged += OnSpeedLevelChanged;
        }

        public void Dispose()
        {
            upgradeButtonsView.AddTrapButtonClicked -= OnTrapClicked;
            upgradeButtonsView.AddMonstersButtonClicked -= OnMonstersClicked;
            upgradeButtonsView.AddSpeedButtonClicked -= OnSpeedClicked;
            upgradeButtonsView.SpeedUpButtonClicked -= OnSpeedUpClicked;

            trap.PriceChanged -= OnTrapPriceChanged;
            trap.LevelChanged -= OnTrapLevelChanged;

            monsters.PriceChanged -= OnMonstersPriceChanged;
            monsters.LevelChanged -= OnMonstersLevelChanged;

            speed.PriceChanged -= OnSpeedPriceChanged;
            speed.LevelChanged -= OnSpeedLevelChanged;
        }

        private void OnTrapClicked()
        {
            trapService.TryAddTrap();
        }

        private void OnMonstersClicked()
        {
            monstersService.TryAddMonsters();
        }

        private void OnSpeedClicked()
        {
            speedService.TryAddSpeed();
        }

        private void OnSpeedUpClicked()
        {
            monsterSpawnService.RegisterSpeedUpTap();
        }

        private void OnTrapPriceChanged(int newPrice)
        {
            ValidatePrice(newPrice);
            upgradeButtonsView.SetAddTrapPriceText(NumberFormatter.Format(newPrice));
        }

        private void OnTrapLevelChanged(int newLevel)
        {
            ValidateLevel(newLevel, CurrentStageSettings.addTrapPrices);
            upgradeButtonsView.SetAddTrapLevelText(NumberFormatter.Format(newLevel));
        }

        private void OnMonstersPriceChanged(int newPrice)
        {
            ValidatePrice(newPrice);
            upgradeButtonsView.SetAddMonstersPriceText(NumberFormatter.Format(newPrice));
        }

        private void OnMonstersLevelChanged(int newLevel)
        {
            ValidateLevel(newLevel, CurrentStageSettings.addMonsterPrices);
            upgradeButtonsView.SetAddMonstersLevelText(NumberFormatter.Format(newLevel));
        }

        private void OnSpeedPriceChanged(int newPrice)
        {
            ValidatePrice(newPrice);
            upgradeButtonsView.SetAddSpeedPriceText(NumberFormatter.Format(newPrice));
        }

        private void OnSpeedLevelChanged(int newLevel)
        {
            ValidateLevel(newLevel, CurrentStageSettings.addSpeedPrices);
            upgradeButtonsView.SetAddSpeedLevelText(NumberFormatter.Format(newLevel));
        }

        private void ValidatePrice(int price)
        {
            if (price < 0)
                throw new ArgumentOutOfRangeException(nameof(price));
        }

        private void ValidateLevel(int level, int[] prices)
        {
            if (prices == null)
                throw new InvalidOperationException("Конфигурация цен не найдена.");

            if (level < 0 || level > prices.Length)
                throw new ArgumentOutOfRangeException(nameof(level));
        }

        public void RefreshView()
        {
            var stageSettings = CurrentStageSettings;

            SyncUpgrade(
                trap.Level,
                trap.Price,
                stageSettings.addTrapPrices,
                upgradeButtonsView.SetAddTrapLevelText,
                upgradeButtonsView.SetAddTrapPriceText);

            SyncUpgrade(
                monsters.Level,
                monsters.Price,
                stageSettings.addMonsterPrices,
                upgradeButtonsView.SetAddMonstersLevelText,
                upgradeButtonsView.SetAddMonstersPriceText);

            SyncUpgrade(
                speed.Level,
                speed.Price,
                stageSettings.addSpeedPrices,
                upgradeButtonsView.SetAddSpeedLevelText,
                upgradeButtonsView.SetAddSpeedPriceText);
        }

        private void SyncUpgrade(
            int level,
            int price,
            int[] prices,
            Action<string> setLevelText,
            Action<string> setPriceText)
        {
            ValidateLevel(level, prices);
            ValidatePrice(price);

            setLevelText(NumberFormatter.Format(level));

            if (level >= prices.Length)
                return;

            setPriceText(NumberFormatter.Format(price));
        }

        private StageSetting CurrentStageSettings => stageCatalog.GetByNumber(stage.Number);
    }
}
