using System;

namespace TrapMaster
{
    [Serializable]
    public sealed class SaveData
    {
        public int coins;
        public int kills;
        public float volume;
        public bool isMusicOn;
        public bool isTrapSoundOn;
        public int addTrapLevel;
        public int addTrapPrice;
        public int addMonstersLevel;
        public int addMonstersPrice;
        public int addSpeedPrice;
        public int addSpeedLevel;
        public int stage;

    }
}