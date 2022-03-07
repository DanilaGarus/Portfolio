using Components.Model.Data;
using Components.Utils.Disposables;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Components.Model
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerData _data;
        public PlayerData Data => _data;
        private PlayerData _save;
        
        private readonly CompositeDisposable _trash = new CompositeDisposable();
        public QuickInventoryModel QuickInventory { get; private set; }

        private void Awake()
        {
            LoadHUD();

            if (IsSessionExit())
            {
                Destroy(gameObject);
            }
            else
            {
                Save();
                InitModels();
                DontDestroyOnLoad(this);
            }
        }

        private void InitModels()
        {
            QuickInventory = new QuickInventoryModel(_data);
            _trash.Retain(QuickInventory);
        }

        private void LoadHUD()
        {
            SceneManager.LoadScene("HUD", LoadSceneMode.Additive);
        }
        
        private bool IsSessionExit()
        {
            var sessions = FindObjectsOfType<GameSession>();
            foreach (var gameSession in sessions)
            {
                if (gameSession != this)
                {
                    return true;
                }
            }

            return false;
        }

        public void Save()
        {
            _save = _data.Clone();
        }
        
        public void LoadLastSave()
        {
            _data = _save.Clone();
            
            _trash.Dispose();
            InitModels();
        }
        
        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}