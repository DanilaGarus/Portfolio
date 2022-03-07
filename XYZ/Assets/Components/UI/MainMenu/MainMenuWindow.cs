using System;
using Components.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Components.UI.MainMenu
{
    public class MainMenuWindow : WindowController
    {
        private Action _closeAnimation;
        
        public void OnShowSettings()
        { 
            WindowUtils.CreateWindow("UI/SettingsWindow");
        }

        public void OnGameStart()
        {
            _closeAnimation = () => { SceneManager.LoadScene("PixelGame"); };
            Close();
        }

        public void OnExit()
        {
            _closeAnimation = () => 
            { 
                Application.Quit();

#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            };
            Close(); 
        }

        public override void OnCloseAnimationComplete()
        {
            _closeAnimation?.Invoke();
            base.OnCloseAnimationComplete(); 
        }
    }
}