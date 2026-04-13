using System;
using Zenject;

namespace TrapMaster
{
    public class KillProgressPresenter : IInitializable, IDisposable
    {
        private const int FallbackTargetKills = 1;

        private IUIKillsView killsView;
        private KillsCounter killsCounter;
        private Wallet wallet;
        private StageCatalog stageCatalog;
        private Stage stage;

        [Inject]
        public void Construct(
            IUIKillsView killsView,
            KillsCounter killsCounter,
            Wallet wallet,
            StageCatalog stageCatalog,
            Stage stage)
        {
            this.killsView = killsView ?? throw new ArgumentNullException(nameof(killsView));
            this.killsCounter = killsCounter ?? throw new ArgumentNullException(nameof(killsCounter));
            this.wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));
            this.stageCatalog = stageCatalog ?? throw new ArgumentNullException(nameof(stageCatalog));
            this.stage = stage ?? throw new ArgumentNullException(nameof(stage));
        }

        public void Initialize()
        {
            killsCounter.Changed += OnKillsChanged;
            wallet.Changed += OnCoinsChanged;
            stage.Changed += OnStageChanged;

            Refresh();
        }

        public void Dispose()
        {
            killsCounter.Changed -= OnKillsChanged;
            wallet.Changed -= OnCoinsChanged;
            stage.Changed -= OnStageChanged;
        }

        private void OnKillsChanged(int _)
        {
            RefreshKills();
        }

        private void OnCoinsChanged(int coins)
        {
            killsView.SetCoinsAmountText(coins);
        }

        private void OnStageChanged(int _)
        {
            RefreshKills();
        }

        private void Refresh()
        {
            RefreshKills();
            killsView.SetCoinsAmountText(wallet.Coins);
        }

        private void RefreshKills()
        {
            int currentKills = killsCounter.CurrentKills;
            int targetKills = GetTargetKills();

            killsView.SetKillsSliderText($"{NumberFormatter.Format(currentKills)}/{NumberFormatter.Format(targetKills)}");
            killsView.SetKillsSliderValue((float)currentKills / targetKills);
        }

        private int GetTargetKills()
        {
            StageSetting stageSetting = stageCatalog.GetByNumber(stage.Number);

            return stageSetting != null && stageSetting.targetKills > 0
                ? stageSetting.targetKills
                : FallbackTargetKills;
        }
    }
}
