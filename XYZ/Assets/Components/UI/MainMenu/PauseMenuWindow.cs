using System;
using Components.Model;
using Components.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Components.UI.MainMenu
{
    public class PauseMenuWindow : WindowController
    {
        private Action _closeAnimation;
        
        private float _defaultTimeScale;

        /*При работе с таймскейл = 0 необходимо ставить в аниматоре апдейт мод
        на анскейл тайм.
        в таком случае игра стопится, апдейты не работают, но анимации
        меню паузы работают*/
        
        protected override void Start()
        {
            base.Start();

           _defaultTimeScale =  Time.timeScale;
           Time.timeScale = 0;
        }
        
        public void OnShowSettings()
        {
            WindowUtils.CreateWindow("UI/SettingsWindow");
        }
        
        public void OnMainMenuExit()
        {
            SceneManager.LoadScene("MainMenu");
            var session = FindObjectOfType<GameSession>();
            Destroy(session.gameObject);
            
        }
        
        private void OnDestroy()
        {
            Time.timeScale = _defaultTimeScale;
        }
    }
}
