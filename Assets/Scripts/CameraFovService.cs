using System;
using UnityEngine;
using Zenject;

namespace TrapMaster
{
    public class CameraFovService : IInitializable, ITickable
    {
        private const float LandscapeFieldOfView = 45f;
        private const float PortraitFieldOfView = 65f;

        private readonly Camera targetCamera;
        private int lastScreenWidth;
        private int lastScreenHeight;

        [Inject]
        public CameraFovService(Camera targetCamera)
        {
            this.targetCamera = targetCamera != null
                ? targetCamera
                : throw new ArgumentNullException(nameof(targetCamera));
        }

        public void Initialize()
        {
            ApplyCurrentFieldOfView(force: true);
        }

        public void Tick()
        {
            ApplyCurrentFieldOfView(force: false);
        }

        private void ApplyCurrentFieldOfView(bool force)
        {
            int currentWidth = Screen.width;
            int currentHeight = Screen.height;

            if (!force &&
                currentWidth == lastScreenWidth &&
                currentHeight == lastScreenHeight)
            {
                return;
            }

            lastScreenWidth = currentWidth;
            lastScreenHeight = currentHeight;

            bool isLandscape = currentWidth >= currentHeight;
            targetCamera.fieldOfView = isLandscape
                ? LandscapeFieldOfView
                : PortraitFieldOfView;
        }
    }
}
