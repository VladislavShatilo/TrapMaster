using System;
using Zenject;

namespace TrapMaster
{
    public class GameBootstrapper : IInitializable
    {
        private SaveService saveService; 
        private StagePresenter stagePresenter;
        private UpgradePresenter upgradePresenter;
        private UpgradeAvailabilityPresenter upgradeAvailabilityPresenter;

        [Inject]
        public void Construct(
            SaveService saveService,
            StagePresenter stagePresenter,
            UpgradePresenter upgradePresenter,
            UpgradeAvailabilityPresenter upgradeAvailabilityPresenter)
        {
            this.saveService = saveService ?? throw new ArgumentNullException(nameof(saveService));
            this.stagePresenter = stagePresenter ?? throw new ArgumentNullException(nameof(stagePresenter));
            this.upgradePresenter = upgradePresenter ?? throw new ArgumentNullException(nameof(upgradePresenter));
            this.upgradeAvailabilityPresenter = upgradeAvailabilityPresenter ?? throw new ArgumentNullException(nameof(upgradeAvailabilityPresenter));
        }

        public void Initialize()
        {
            saveService.InitializeGame();
            stagePresenter.UpdateStageText();
            upgradePresenter.RefreshView();
            upgradeAvailabilityPresenter.RefreashAll();
        }
    }

}
