using System;

namespace TrapMaster
{
    public abstract class UpgradeBase
    {
        public int Level { get; private set; }
        public int Price { get; private set; }

        public event Action<int> LevelChanged;
        public event Action<int> PriceChanged;

        public void IncreaseLevel()
        {
            SetLevel(Level + 1);
        }

        public void IncreasePrice(int amount)
        {
            ValidatePositive(amount, nameof(amount));
            SetPrice(Price + amount);
        }

        public void SetLevel(int level)
        {
            ValidateNonNegative(level, nameof(level));

            if (Level == level)
                return;

            Level = level;
            LevelChanged?.Invoke(Level);
        }

        public void SetPrice(int price)
        {
            ValidateNonNegative(price, nameof(price));

            if (Price == price)
                return;

            Price = price;
            PriceChanged?.Invoke(Price);
        }

        private void ValidateNonNegative(int value, string paramName)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(paramName, "Value не может быть меньше 0");
        }

        private void ValidatePositive(int value, string paramName)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(paramName, "Value должно быть больше 0");
        }
    }
}