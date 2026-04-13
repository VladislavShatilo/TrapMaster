using UnityEngine;

namespace TrapMaster
{
    public interface IUIKillsView 
    {
        void SetKillsSliderText(string kills);
        void SetCoinsAmountText(int coins);
        void SetKillsSliderValue(float normalizedValue);
    }

}