using System;
using Zenject;

namespace TrapMaster
{
    public class StageService
    {
        private const int FirstStageNumber = 1;
        private const int LastStageNumber = 3;

        private Stage stage;
        private StageCatalog stageCatalog;
        private SaveService saveService;
        private TrapService trapService;
        private MonsterSpawnService monsterSpawnService;

        [Inject]
        public void Construct(
            Stage stage,
            SaveService saveService,
            TrapService trapService,
            MonsterSpawnService monsterSpawnService,
            StageCatalog stageCatalog)
        {
            this.stage = stage ?? throw new ArgumentNullException(nameof(stage));
            this.saveService = saveService ?? throw new ArgumentNullException(nameof(saveService));
            this.trapService = trapService ?? throw new ArgumentNullException(nameof(trapService));
            this.monsterSpawnService = monsterSpawnService ?? throw new ArgumentNullException(nameof(monsterSpawnService));
            this.stageCatalog = stageCatalog ?? throw new ArgumentNullException(nameof(stageCatalog));
        }

        public void NewStage()
        {
            trapService.DestroyAllTraps();
            monsterSpawnService.DestroyAllMonsters();

            int nextStageNumber = stage.Number >= LastStageNumber
                ? FirstStageNumber
                : stage.Number + 1;

            saveService.ResetSave(nextStageNumber);
        }

        public void CheckKills(int allKills)
        {
            var stageConfig = stageCatalog.GetByNumber(stage.Number);

            if (allKills >= stageConfig.targetKills)
            {
                stage.Complete();
            }
        }
    }
}
