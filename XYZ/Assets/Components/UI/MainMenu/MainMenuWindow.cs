using System;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Components.UI.MainMenu
{
    public class MainMenuWindow : WindowController
    {
        private Action _closeAnimation;
        
        public void OnShowSettings()
        {
            var window = Resources.Load<GameObject>("UI/SettingsWindow");
            var canvas = FindObjectOfType<Canvas>();
            Instantiate(window, canvas.transform);
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
            base.OnCloseAnimationComplete();
            
            _closeAnimation?.Invoke();
        }
    }
}