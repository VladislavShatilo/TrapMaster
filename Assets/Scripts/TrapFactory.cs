using System;
using UnityEngine;
using Zenject;

namespace TrapMaster
{
    public class TrapFactory : ITrapFactory
    {
        private DiContainer container;
        private StageCatalog stageCatalog;
        private Stage stage;

        [Inject]
        public void Construct(DiContainer container, StageCatalog stageCatalog, Stage stage)
        {
            this.container = container ?? throw new ArgumentNullException(nameof(container));
            this.stageCatalog = stageCatalog ?? throw new ArgumentNullException(nameof(stageCatalog));
            this.stage = stage ?? throw new ArgumentNullException(nameof(stage));
        }

        public ITrap AddTrap(Vector3 position, Quaternion rotation)
        {
            var prefab = stageCatalog.GetByNumber(stage.Number).trapPrefab;
            return container.InstantiatePrefabForComponent<ITrap>(prefab, position, rotation, null);
        }

        public void DestroyTrap(ITrap trap)
        {
            if (trap == null)
                throw new ArgumentNullException(nameof(trap));

            if (trap is not Component component)
                throw new InvalidOperationException("ITrap должен реализовываться через Component / MonoBehaviour.");

            UnityEngine.Object.Destroy(component.gameObject);
        }
    }
}