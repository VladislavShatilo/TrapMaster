using System;

namespace TrapMaster
{
    public class Speed : UpgradeBase
    {
        public float Value { get; private set; }

        public event Action<float> Changed;

        public void Set(float speed)
        {
            Validate(speed);

            if (Value == speed)
                return;

            Value = speed;
            Changed?.Invoke(Value);
        }

        private void Validate(float value)
        {
            if (value < 0f)
                throw new ArgumentException("Speed must be >= 0", nameof(value));
        }
    }
}