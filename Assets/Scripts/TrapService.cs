using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TrapMaster
{
    public class TrapService
    {
        private Trap trap;
        private ITrapFactory trapFactory;
        private Wallet wallet;
        private StageCatalog stageCatalog;
        private Stage stage;
        private readonly List<ITrap> traps = new();

        [Inject]
        public void Construct(
            Trap trap,
            ITrapFactory trapFactory,
            Wallet wallet,
            StageCatalog stageCatalog,
            Stage stage)
        {
            this.trap = trap ?? throw new ArgumentNullException(nameof(trap));
            this.trapFactory = trapFactory ?? throw new ArgumentNullException(nameof(trapFactory));
            this.wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));
            this.stageCatalog = stageCatalog ?? throw new ArgumentNullException(nameof(stageCatalog));
            this.stage = stage ?? throw new ArgumentNullException(nameof(stage));
        }

        public bool TryAddTrap()
        {
            var stageConfig = stageCatalog.GetByNumber(stage.Number);
            var trapPrices = stageConfig.addTrapPrices;
            var trapPositions = stageConfig.trapPositions;

            if (trap.Level >= trapPrices.Length)
                return false;

            var nextTrapIndex = trap.Level;

            if (nextTrapIndex >= trapPositions.Length)
                throw new InvalidOperationException("Недостаточно позиций для ловушек.");

            if (!wallet.TrySpendCoins(trap.Price))
                return false;

            var createdTrap = trapFactory.AddTrap(
                trapPositions[nextTrapIndex],
                Quaternion.identity);

            traps.Add(createdTrap);
            trap.IncreaseLevel();

            if (trap.Level < trapPrices.Length)
            {
                trap.SetPrice(trapPrices[trap.Level]);
            }

            return true;
        }

        public void LoadTraps(int trapLevel)
        {
            var trapPrices = stageCatalog.GetByNumber(stage.Number).addTrapPrices;

            if (trapLevel < 0 || trapLevel > trapPrices.Length)
                throw new ArgumentOutOfRangeException(nameof(trapLevel));

            RebuildTraps(trapLevel);
        }

        public void DestroyAllTraps()
        {
            foreach (var trap in traps)
            {
                trapFactory.DestroyTrap(trap);
            }

            traps.Clear();
        }

        private void RebuildTraps(int level)
        {
            DestroyAllTraps();

            var trapPositions = stageCatalog.GetByNumber(stage.Number).trapPositions;

            if (level > trapPositions.Length)
                throw new InvalidOperationException("Недостаточно позиций для ловушек.");

            for (var i = 0; i < level; i++)
            {
                var createdTrap = trapFactory.AddTrap(
                    trapPositions[i],
                    Quaternion.identity);

                traps.Add(createdTrap);
            }
        }
    }
}