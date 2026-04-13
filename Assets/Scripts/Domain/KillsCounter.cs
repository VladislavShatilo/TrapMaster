using System;

namespace TrapMaster
{
    public class KillsCounter
    {
        public event Action<int> Changed;

        public int CurrentKills { get; private set; }

        public void AddKills(int kills)
        {
            ValidateKills(kills);
            UpdateKills(CurrentKills + kills);
        }

        public void SetKills(int kills)
        {
            ValidateKills(kills);
            UpdateKills(kills);
        }

        public void ResetKills()
        {
            UpdateKills(0);
        }

        private void UpdateKills(int newValue)
        {
            CurrentKills = newValue;
            Changed?.Invoke(CurrentKills);
        }

        private void ValidateKills(int kills)
        {
            if (kills < 0)
                throw new ArgumentException("Kills не может быть меньше нуля", nameof(kills));
        }
    }
}