using System;

namespace TrapMaster
{
    public class Stage
    {
        public event Action<int> Changed;
        public event Action Completed;

        public int Number { get; private set; }

        public void NextStage()
        {
            SetStage(Number + 1);
        }

        public void Complete()
        {
            Completed?.Invoke();
        }

        public void SetStage(int stage)
        {
            ValidateStage(stage);

            if (Number == stage)
                return;

            Number = stage;
            Changed?.Invoke(Number);
        }

        private void ValidateStage(int stage)
        {
            if (stage < 0)
                throw new ArgumentOutOfRangeException(nameof(stage), "Stage не может быть меньше 0");
        }
    }
}