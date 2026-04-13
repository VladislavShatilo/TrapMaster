using UnityEngine;
using YG;

namespace TrapMaster
{
    public sealed class YGSaveStorage : ISaveStorage
    {
        public void Save(SaveData data)
        {
            if (data == null)
            {
                Debug.LogError("YGSaveStorage: save data is null.");
                return;
            }

            YG2.saves.hasTrapMasterSave = true;
            YG2.saves.trapMasterSave = Clone(data);

            if (YG2.isSDKEnabled)
                YG2.SaveProgress();
        }

        public SaveData Load()
        {
            if (!Exists())
                return new SaveData();

            return Clone(YG2.saves.trapMasterSave);
        }

        public bool Exists()
        {
            return YG2.saves != null &&
                   YG2.saves.hasTrapMasterSave &&
                   YG2.saves.trapMasterSave != null;
        }

        private static SaveData Clone(SaveData data)
        {
            return new SaveData
            {
                coins = data.coins,
                kills = data.kills,
                volume = data.volume,
                isMusicOn = data.isMusicOn,
                isTrapSoundOn = data.isTrapSoundOn,
                addTrapLevel = data.addTrapLevel,
                addTrapPrice = data.addTrapPrice,
                addMonstersLevel = data.addMonstersLevel,
                addMonstersPrice = data.addMonstersPrice,
                addSpeedPrice = data.addSpeedPrice,
                addSpeedLevel = data.addSpeedLevel,
                stage = data.stage,
            };
        }
    }
}
