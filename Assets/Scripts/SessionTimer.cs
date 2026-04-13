using System;
using UnityEngine;
using Zenject;

namespace TrapMaster
{
    public class SessionTimer : ITickable
    {
        public float ElapsedTime { get; private set; }

        public void Tick()
        {
            ElapsedTime += Time.deltaTime;
        }

        public void Reset()
        {
            ElapsedTime = 0f;
        }
    }
}