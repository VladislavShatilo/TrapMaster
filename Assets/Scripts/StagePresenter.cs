using System;
using UnityEngine;
using Zenject;

namespace TrapMaster
{
    public class StagePresenter : IInitializable, IDisposable
    {
        private StageService stageService;
        private Stage stage;
        private StageView stageView;

        [Inject]
        public void Construct(StageService stageService, Stage stage, StageView stageView)
        {
            this.stageService = stageService ?? throw new ArgumentNullException(nameof(stageService));
            this.stage = stage ?? throw new ArgumentNullException(nameof(stage));
            this.stageView = stageView ?? throw new ArgumentNullException(nameof(stageView));
        }

        public void Initialize()
        {
            stage.Completed += ShowStageWindow;
            stageView.NextStageClicked += OnNextStageClicked;
        }

        public void Dispose()
        {
            stage.Completed -= ShowStageWindow;
            stageView.NextStageClicked -= OnNextStageClicked;
        }

        private void ShowStageWindow()
        {
            stageView.Show();
        }

        private void OnNextStageClicked()
        {
            stageView.Hide();
            stageService.NewStage();
            UpdateStageText();
        }

        public void UpdateStageText()
        {
            stageView.SetStageText(stage.Number);
        }
    }
}