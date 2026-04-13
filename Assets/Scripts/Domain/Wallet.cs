using System;

namespace TrapMaster
{
    public class Wallet
    {
        public event Action<int> Changed;

        public int Coins { get; private set; }

        public void AddCoins(int amount)
        {
            ValidateNonNegative(amount);
            UpdateCoins(Coins + amount);
        }

        public void SetCoins(int amount)
        {
            ValidateNonNegative(amount);
            UpdateCoins(amount);
        }

        public bool TrySpendCoins(int amount)
        {
            ValidateNonNegative(amount);

            if (amount > Coins)
                return false;

            UpdateCoins(Coins - amount);
            return true;
        }

        public void ResetCoins()
        {
            UpdateCoins(0);
        }

        private void UpdateCoins(int newValue)
        {
            if (Coins == newValue)
                return;

            Coins = newValue;
            Changed?.Invoke(Coins);
        }

        private void ValidateNonNegative(int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Количество монет не может быть меньше 0");
        }
    }
}