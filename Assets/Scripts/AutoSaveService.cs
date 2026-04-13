using System;
using UnityEngine;
using Zenject;

namespace TrapMaster
{
    public class AutoSaveService : ITickable
    {
        private SaveService saveService;
        private SaveSettings saveSettings;
        private float elapsedTime;

        [Inject]
        public void Construct(SaveService saveService, SaveSettings saveSettings)
        {
            this.saveService = saveService ?? throw new ArgumentNullException(nameof(saveService));
            this.saveSettings = saveSettings ?? throw new ArgumentNullException(nameof(saveSettings));
        }

        public void Tick()
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime < saveSettings.saveInterval)
                return;

            elapsedTime -= saveSettings.saveInterval;
            saveService.Save();
        }
    }
}