using Components.Model.Data;
using Components.UI.Widgets;
using UnityEngine;

namespace Components.UI.Settings
{
    public class SettingsWindow : WindowController
    {
        [SerializeField] private AudioSettingWidjet _music;
        [SerializeField] private AudioSettingWidjet _sfx;
        
        protected override void Start()
        {
            base.Start();
            _music.SetModeel(GameSettings.I.Music);
            _sfx.SetModeel(GameSettings.I.Sfx);
        }
    }
}