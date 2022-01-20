using Components.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Components.LevelManagment
{
    public class LevelExitComponent : MonoBehaviour
    {
        [SerializeField] private string _sceneName;

        public void Exit()
        {
            var session = FindObjectOfType<GameSession>();
            session.Save();
            SceneManager.LoadScene(_sceneName);  
        }
    }
}

