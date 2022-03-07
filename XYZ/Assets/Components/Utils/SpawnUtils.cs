using UnityEngine;

namespace Components.Utils
{
    public class SpawnUtils
    {
        private const string ContsinerName = "|||SPAWNED|||";

        public static GameObject Spawn(GameObject prefab, Vector3 position)
        {
            var container = GameObject.Find(ContsinerName);
            if (container == null)
                container = new GameObject(ContsinerName);
            
            return Object.Instantiate(prefab, position, Quaternion.identity, container.transform);
        }
    }
}