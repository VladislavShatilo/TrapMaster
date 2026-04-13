using System;
using Zenject;

namespace TrapMaster
{
    public class KillRewardService
    {
        private Wallet wallet;
        private KillsCounter killsCounter;
        private StageService stageService;

        [Inject]
        public void Construct(
            Wallet wallet,
            KillsCounter killsCounter,
            StageService stageService)
        {
            this.wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));
            this.killsCounter = killsCounter ?? throw new ArgumentNullException(nameof(killsCounter));
            this.stageService = stageService ?? throw new ArgumentNullException(nameof(stageService));
        }

        public void RegisterKill(int reward)
        {
            killsCounter.AddKills(reward);
            wallet.AddCoins(reward);
            stageService.CheckKills(killsCounter.CurrentKills);
        }
    }
}