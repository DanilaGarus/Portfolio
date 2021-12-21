using UnityEngine;

namespace Components.GameObjectBased
{
    public class ObjectDestroyComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _ObjectToDestroy;
        public void Destroy()
        {
            Destroy(_ObjectToDestroy);  
        }
    }
}