using System;
using UnityEngine;

namespace TrapMaster
{
    public interface ISettingsPopupView 
    {
        event Action CloseClicked;
        event Action<bool> MusicChanged;
        event Action<bool> TrapAudioChanged;

        void Show();
        void Hide();

        public void SetMusicSwitchOn(bool isOn);
        public void SetTrapSoundSwitchOn(bool isOn);
  
    }

}