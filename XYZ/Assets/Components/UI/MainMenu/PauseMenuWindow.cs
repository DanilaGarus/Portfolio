using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Components.UI.MainMenu
{
    public class PauseMenuWindow : WindowController
    {
        private Action _closeAnimation;
        
        public void OnShowSettings()
        {
            var window = Resources.Load<GameObject>("UI/SettingsWindow");
            var canvas = FindObjectOfType<Canvas>();
            Instantiate(window, canvas.transform);
        }

        public void OnGameContinue()
        {
            var menu = FindObjectOfType<PauseMenuWindow>();
            Close();
        }

        public void OnMainMenuExit()
        {
            _closeAnimation = () =>
            {
                SceneManager.LoadScene("MainMenu");
            };
            Close();
        }

        public override void OnCloseAnimationComplete()
        {
            _closeAnimation?.Invoke();
            base.OnCloseAnimationComplete(); }
    } 
}
