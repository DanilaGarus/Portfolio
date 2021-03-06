using Components.Utils;
using UnityEngine;

namespace Components.GameObjectBased
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private GameObject _prefab;

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            var instantiate = SpawnUtils.Spawn(_prefab, _target.position);
            
            var scale = _target.lossyScale;
            instantiate.transform.localScale = scale;
            instantiate.SetActive(true);
        }

        public void SetPrefab(GameObject prefab)
        {
            _prefab = prefab;
        }
    }
}

 