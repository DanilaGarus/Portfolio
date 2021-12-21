using Components.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Components.LevelManagment
{

    public class LevelReloadComp : MonoBehaviour
    {
        public void LVL_Reload()
        {
           var session = FindObjectOfType<GameSession>();
            session.LoadLastSave();
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

}
