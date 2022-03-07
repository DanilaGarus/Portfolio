using Components.Model;
using Components.Model.Definitions;
using Components.UI.Widgets;
using Components.Utils;
using UnityEngine;

namespace Components.UI.HUD
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] private ProgressBarWidget _healthBar;

        private GameSession _session;
        
        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            _session.Data._hp.OnChanged += OnHealthChanged;
            
            OnHealthChanged(_session.Data._hp.Value, 0);
        }

        private void OnHealthChanged(int newValue, int oldValue)
        {
            var maxHealth = DefsFacade.I.PlayerDef.MaxHealth;
            var value = (float) newValue / maxHealth;
            _healthBar.SetProgress(value);
        }
        
        public void OnPause()
        {
            WindowUtils.CreateWindow("UI/PauseMenuWindow");
        }

        private void OnDestroy()
        {
            _session.Data._hp.OnChanged -= OnHealthChanged;
        }
    }
}