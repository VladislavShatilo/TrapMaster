using System;
using UnityEngine;
using Zenject;

namespace TrapMaster
{
    public sealed class SaveOnApplicationQuit : MonoBehaviour
    {
        private SaveService saveService;

        [Inject]
        public void Construct(SaveService saveService)
        {
            this.saveService = saveService?? throw new ArgumentNullException(nameof(saveService));
        }

        private void OnApplicationQuit()
        {
            saveService.Save();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
                saveService.Save();
        }
    }
}