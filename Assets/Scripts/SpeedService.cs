using System;
using Zenject;

namespace TrapMaster
{
    public class SpeedService
    {
        private Speed speed;
        private Wallet wallet;
        private Stage stage;
        private StageCatalog stageCatalog;

        [Inject]
        public void Construct(
            Speed speed,
            Wallet wallet,
            Stage stage,
            StageCatalog stageCatalog)
        {
            this.speed = speed ?? throw new ArgumentNullException(nameof(speed));
            this.wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));
            this.stage = stage ?? throw new ArgumentNullException(nameof(stage));
            this.stageCatalog = stageCatalog ?? throw new ArgumentNullException(nameof(stageCatalog));
        }

        public bool TryAddSpeed()
        {
            var stageConfig = GetCurrentStageConfig();
            var addSpeedPrices = stageConfig.addSpeedPrices;

            if (speed.Level >= addSpeedPrices.Length)
                return false;

            var nextLevelPrice = speed.Price;

            if (wallet.Coins < nextLevelPrice)
                return false;

            if (!wallet.TrySpendCoins(nextLevelPrice))
                return false;

            speed.IncreaseLevel();

            if (speed.Level < addSpeedPrices.Length)
            {
                speed.SetPrice(addSpeedPrices[speed.Level]);
            }

            SetCurrentSpeed();
            return true;
        }

        public void SetCurrentSpeed()
        {
            var stageConfig = GetCurrentStageConfig();
            var currentSpeed = stageConfig.baseSpeed *
                               (float)Math.Pow(stageConfig.speedMultiplier, speed.Level);

            speed.Set(currentSpeed);
        }

        public void ResetSpeed()
        {
            var stageConfig = GetCurrentStageConfig();
            speed.Set(stageConfig.baseSpeed);
        }

        private StageSetting GetCurrentStageConfig()
        {
            return stageCatalog.GetByNumber(stage.Number);
        }
    }
}
