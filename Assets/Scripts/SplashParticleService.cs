using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TrapMaster
{
    public class SplashParticleService : MonoBehaviour
    {
        private const float DefaultLifetime = 0.5f;

        private SplashSettings splashSettings;
        private readonly Queue<GameObject> availableParticles = new();
        private readonly HashSet<GameObject> rentedParticles = new();

        private GameObject ParticlePrefab => splashSettings.particleSplashObject;

        [Inject]
        public void Construct(SplashSettings splashSettings)
        {
            this.splashSettings = splashSettings ?? throw new ArgumentNullException(nameof(splashSettings));
        }

        private void Awake()
        {
            WarmupPool();
        }

        public void CreateSplash(Vector3 splashPosition)
        {
            if (ParticlePrefab == null)
                return;

            var particle = GetParticleInstance();
            if (particle == null)
                return;

            var spawnPosition = new Vector3(
                splashPosition.x,
                splashSettings.particleSplashPositionY,
                splashPosition.z);

            particle.transform.SetPositionAndRotation(
                spawnPosition,
                ParticlePrefab.transform.rotation);

            particle.SetActive(true);
            RestartParticleSystems(particle);

            var lifetime = GetLifetime(particle);
            StartCoroutine(ReturnToPoolAfterDelay(particle, lifetime));
        }

        private void WarmupPool()
        {
            if (ParticlePrefab == null)
                return;

            var poolSize = Mathf.Max(0, splashSettings.initialPoolSize);

            for (var i = 0; i < poolSize; i++)
            {
                availableParticles.Enqueue(CreatePooledParticle());
            }
        }

        private GameObject GetParticleInstance()
        {
            if (availableParticles.Count > 0)
            {
                var pooledParticle = availableParticles.Dequeue();
                rentedParticles.Add(pooledParticle);
                return pooledParticle;
            }

            if (HasReachedPoolLimit())
                return null;

            var createdParticle = CreatePooledParticle();
            rentedParticles.Add(createdParticle);
            return createdParticle;
        }

        private bool HasReachedPoolLimit()
        {
            if (splashSettings.maxPoolSize <= 0)
                return false;

            var totalCount = availableParticles.Count + rentedParticles.Count;
            return totalCount >= splashSettings.maxPoolSize;
        }

        private GameObject CreatePooledParticle()
        {
            var particle = Instantiate(ParticlePrefab, transform);
            ConfigureParticleSystems(particle);
            particle.SetActive(false);
            return particle;
        }

        private IEnumerator ReturnToPoolAfterDelay(GameObject particle, float delay)
        {
            yield return new WaitForSeconds(delay);

            if (particle == null)
                yield break;

            if (!rentedParticles.Remove(particle))
                yield break;

            StopParticleSystems(particle);
            particle.SetActive(false);
            availableParticles.Enqueue(particle);
        }

        private void RestartParticleSystems(GameObject particle)
        {
            var particleSystems = particle.GetComponentsInChildren<ParticleSystem>(true);

            for (var i = 0; i < particleSystems.Length; i++)
            {
                particleSystems[i].Clear(true);
                particleSystems[i].Play(true);
            }
        }

        private void StopParticleSystems(GameObject particle)
        {
            var particleSystems = particle.GetComponentsInChildren<ParticleSystem>(true);

            for (var i = 0; i < particleSystems.Length; i++)
            {
                particleSystems[i].Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            }
        }

        private void ConfigureParticleSystems(GameObject particle)
        {
            var particleSystems = particle.GetComponentsInChildren<ParticleSystem>(true);

            for (var i = 0; i < particleSystems.Length; i++)
            {
                var main = particleSystems[i].main;
                main.stopAction = ParticleSystemStopAction.None;
                main.playOnAwake = false;
            }
        }

        private float GetLifetime(GameObject particle)
        {
            var particleSystems = particle.GetComponentsInChildren<ParticleSystem>(true);
            var maxLifetime = DefaultLifetime;

            for (var i = 0; i < particleSystems.Length; i++)
            {
                var main = particleSystems[i].main;
                var systemLifetime = main.duration + GetMaxStartDelay(main.startDelay);

                systemLifetime += GetMaxStartLifetime(main.startLifetime);

                maxLifetime = Mathf.Max(maxLifetime, systemLifetime);
            }

            return maxLifetime;
        }

        private float GetMaxStartDelay(ParticleSystem.MinMaxCurve startDelay)
        {
            return startDelay.mode == ParticleSystemCurveMode.TwoConstants
                ? startDelay.constantMax
                : startDelay.constant;
        }

        private float GetMaxStartLifetime(ParticleSystem.MinMaxCurve startLifetime)
        {
            return startLifetime.mode == ParticleSystemCurveMode.TwoConstants
                ? startLifetime.constantMax
                : startLifetime.constant;
        }
    }
}
