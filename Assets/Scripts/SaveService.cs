using System;
using UnityEngine;
using Zenject;

namespace TrapMaster
{
    public class SaveService 
    {
        private const int DefaultStageNumber = 1;
        private const float DefaultVolume = 1f;
        private const bool DefaultMusicEnabled = true;
        private const bool DefaultTrapSoundEnabled = true;

        private Wallet wallet;
        private Sound sound;
        private KillsCounter killsCounter;
        private Trap trap;
        private Monsters monsters;
        private Speed speed;
        private Stage stage;
        private ISaveStorage storage;
        private TrapService trapService;
        private SpeedService speedService;
        private MonsterSpawnService monsterSpawnService;
        private StageCatalog stageCatalog;
        private bool isDirty;
        private bool isApplyingSaveData;

        [Inject]
        public void Construct(Wallet wallet, KillsCounter killCounter, ISaveStorage storage, Sound sound, Trap addTrap,
            TrapService trapService, Monsters monsters, MonsterSpawnService monsterSpawnService,
            Speed speed, SpeedService speedService, Stage stage, StageCatalog stageCatalog)
        {
            this.wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));
            this.killsCounter = killCounter ?? throw new ArgumentNullException(nameof(killCounter));
            this.storage = storage ?? throw new ArgumentNullException(nameof(storage));
            this.sound = sound ?? throw new ArgumentNullException(nameof(sound));
            this.trap = addTrap ?? throw new ArgumentNullException(nameof(addTrap));
            this.trapService = trapService ?? throw new ArgumentNullException(nameof(trapService));
            this.monsters = monsters ?? throw new ArgumentNullException(nameof(monsters));
            this.monsterSpawnService = monsterSpawnService ?? throw new ArgumentNullException(nameof(monsterSpawnService));
            this.speed = speed ?? throw new ArgumentNullException(nameof(speed));
            this.speedService = speedService ?? throw new ArgumentNullException(nameof(speedService));
            this.stage = stage ?? throw new ArgumentNullException(nameof(stage));
            this.stageCatalog = stageCatalog ?? throw new ArgumentNullException(nameof(stageCatalog));

            wallet.Changed += _ => MarkDirty();
            killsCounter.Changed += _ => MarkDirty();
            sound.VolumeChanged += _ => MarkDirty();
            sound.MusicChanged += _ => MarkDirty();
            sound.TrapSoundChanged += _ => MarkDirty();
            trap.LevelChanged += _ => MarkDirty();
            trap.PriceChanged += _ => MarkDirty();
            monsters.LevelChanged += _ => MarkDirty();
            monsters.PriceChanged += _ => MarkDirty();
            speed.LevelChanged += _ => MarkDirty();
            speed.PriceChanged += _ => MarkDirty();
            stage.Changed += _ => MarkDirty();
        }

        public void InitializeGame()
        {
            if (storage.Exists())
            {
                LoadExistingGame();
                return;
            }

            StartNewGame();
        }

        public void Save()
        {
            if (!isDirty)
                return;

            SaveData data = new SaveData
            {
                coins = wallet.Coins,
                kills = killsCounter.CurrentKills,
                volume = sound.Volume,
                isMusicOn = sound.IsMusicOn,
                isTrapSoundOn = sound.IsTrapSoundOn,
                addTrapLevel = trap.Level,
                addTrapPrice = trap.Price,
                addMonstersLevel = monsters.Level,
                addMonstersPrice = monsters.Price,
                addSpeedPrice = speed.Price,
                addSpeedLevel = speed.Level,
                stage = stage.Number,

            };
            storage.Save(data);
            isDirty = false;
        }

        public void ResetSave(int stageNumber)
        {
            SaveData data = CreateNewGameData(stageNumber);
            storage.Save(data);
            ApplySaveData(data);
        }

        private void StartNewGame()
        {
            SaveData data = CreateNewGameData(DefaultStageNumber);
            storage.Save(data);
            isDirty = false;
            ApplySaveData(data);
        }

        private void LoadExistingGame()
        {
            SaveData data = storage.Load();
            data = NormalizeLoadedData(data);
            ApplySaveData(data);
        }

        private SaveData CreateNewGameData(int stageNumber)
        {
            StageSetting stageSetting = GetStageSetting(stageNumber);

            return new SaveData
            {
                coins = 0,
                kills = 0,
                volume = DefaultVolume,
                isMusicOn = DefaultMusicEnabled,
                isTrapSoundOn = DefaultTrapSoundEnabled,
                addTrapLevel = 0,
                addTrapPrice = GetFirstPrice(stageSetting.addTrapPrices, nameof(stageSetting.addTrapPrices)),
                addMonstersLevel = 1,
                addMonstersPrice = GetFirstPrice(stageSetting.addMonsterPrices, nameof(stageSetting.addMonsterPrices)),
                addSpeedLevel = 1,
                addSpeedPrice = GetFirstPrice(stageSetting.addSpeedPrices, nameof(stageSetting.addSpeedPrices)),
                stage = stageNumber,
            };
        }

        private void ApplySaveData(SaveData data)
        {
            StageSetting stageSetting = GetStageSetting(data.stage);
            isApplyingSaveData = true;

            try
            {
                stage.SetStage(stageSetting.stageNumber);
                speedService.ResetSpeed();

                wallet.SetCoins(Mathf.Max(0, data.coins));
                killsCounter.SetKills(Mathf.Max(0, data.kills));
                sound.LoadSound(data.volume, data.isMusicOn, data.isTrapSoundOn);

                trap.SetLevel(data.addTrapLevel);
                trap.SetPrice(data.addTrapPrice);
                monsters.SetLevel(data.addMonstersLevel);
                monsters.SetPrice(data.addMonstersPrice);
                speed.SetLevel(data.addSpeedLevel);
                speed.SetPrice(data.addSpeedPrice);

                trapService.LoadTraps(data.addTrapLevel);
                monsterSpawnService.UpdateSpawnInterval();
                speedService.SetCurrentSpeed();
            }
            finally
            {
                isApplyingSaveData = false;
                isDirty = false;
            }
        }

        private void MarkDirty()
        {
            if (isApplyingSaveData)
                return;

            isDirty = true;
        }

        private SaveData NormalizeLoadedData(SaveData data)
        {
            if (data == null)
                return CreateNewGameData(DefaultStageNumber);

            int normalizedStageNumber = stageCatalog.GetByNumber(data.stage) != null
                ? data.stage
                : DefaultStageNumber;

            StageSetting stageSetting = GetStageSetting(normalizedStageNumber);

            data.stage = normalizedStageNumber;
            data.coins = Mathf.Max(0, data.coins);
            data.kills = Mathf.Max(0, data.kills);
            data.volume = Mathf.Clamp01(data.volume);

            data.addTrapLevel = Mathf.Clamp(data.addTrapLevel, 0, stageSetting.trapPositions.Length);
            data.addMonstersLevel = ClampUpgradeLevel(data.addMonstersLevel, stageSetting.addMonsterPrices);
            data.addSpeedLevel = ClampUpgradeLevel(data.addSpeedLevel, stageSetting.addSpeedPrices);

            data.addTrapPrice = NormalizePrice(data.addTrapPrice, stageSetting.addTrapPrices, data.addTrapLevel);
            data.addMonstersPrice = NormalizePrice(data.addMonstersPrice, stageSetting.addMonsterPrices, data.addMonstersLevel);
            data.addSpeedPrice = NormalizePrice(data.addSpeedPrice, stageSetting.addSpeedPrices, data.addSpeedLevel);

            return data;
        }

        private int ClampUpgradeLevel(int level, int[] prices)
        {
            if (prices == null || prices.Length == 0)
                throw new InvalidOperationException("Upgrade prices are not configured.");

            return Mathf.Clamp(level, 0, prices.Length);
        }

        private int NormalizePrice(int savedPrice, int[] prices, int level)
        {
            if (savedPrice >= 0)
                return savedPrice;

            return GetPriceForLevel(prices, level);
        }

        private int GetPriceForLevel(int[] prices, int level)
        {
            if (prices == null || prices.Length == 0)
                throw new InvalidOperationException("Upgrade prices are not configured.");

            int clampedLevel = Mathf.Clamp(level, 0, prices.Length - 1);
            return prices[clampedLevel];
        }

        private int GetFirstPrice(int[] prices, string fieldName)
        {
            if (prices == null || prices.Length == 0)
                throw new InvalidOperationException($"{fieldName} is not configured.");

            return prices[0];
        }

        private StageSetting GetStageSetting(int stageNumber)
        {
            StageSetting stageSetting = stageCatalog.GetByNumber(stageNumber);
            if (stageSetting == null)
                throw new InvalidOperationException($"Stage config for stage {stageNumber} is missing.");

            if (stageSetting.trapPositions == null)
                throw new InvalidOperationException($"Trap positions are not configured for stage {stageNumber}.");

            return stageSetting;
        }
    }

}
