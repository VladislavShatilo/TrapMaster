using System;
using Zenject;

namespace TrapMaster
{
    public class MonsterKillHandler : IInitializable, IDisposable
    {
        private EventBus eventBus;
        private KillRewardService killRewardService;
        private SplashParticleService splashParticleService;
        private AudioPlayer audioPlayer;
        private Stage stage;

        [Inject]
        public void Construct(
            EventBus eventBus,
            KillRewardService killRewardService,
            SplashParticleService splashParticleService,
            AudioPlayer audioPlayer,
            Stage stage)
        {
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            this.killRewardService = killRewardService ?? throw new ArgumentNullException(nameof(killRewardService));
            this.splashParticleService = splashParticleService ?? throw new ArgumentNullException(nameof(splashParticleService));
            this.audioPlayer = audioPlayer ?? throw new ArgumentNullException(nameof(audioPlayer));
            this.stage = stage ?? throw new ArgumentNullException(nameof(stage));
        }

        public void Initialize()
        {
            eventBus.Subscribe<MonsterDie>(OnMonsterKilled);
        }

        public void Dispose()
        {
            eventBus.Unsubscribe<MonsterDie>(OnMonsterKilled);
        }

        private void OnMonsterKilled(MonsterDie monsterDie)
        {
            int reward = GetReward(monsterDie);

            killRewardService.RegisterKill(reward);
            splashParticleService.CreateSplash(monsterDie.DiePosition);
            audioPlayer.PlayTrapSound();
        }

        private int GetReward(MonsterDie monsterDie)
        {
            return monsterDie.isX3
                ? stage.Number * 3
                : stage.Number;
        }
    }
}