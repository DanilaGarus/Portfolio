using System;
using UnityEngine;
using UnityEngine.Events;

namespace Components.World_Scripts
{


    public class CollisionForPoision : MonoBehaviour
    {
        [SerializeField] private string _PoisionTag;
        [SerializeField] private UnityEvent _Event;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(_PoisionTag))
            {
                
                _Event?.Invoke(other.gameObject);
                StartCoroutine("OnPoisionDamage");
            }
        }

        [Serializable]
        public class UnityEvent : UnityEvent<GameObject>
        {

        }
    }
}