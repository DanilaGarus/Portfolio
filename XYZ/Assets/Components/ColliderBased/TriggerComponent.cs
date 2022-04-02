using System;
using Components.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace Components.ColliderBased
{
    public class TriggerComponent : MonoBehaviour
    {
        [SerializeField] private EnterEvent _action;
        [SerializeField] private string _tag;
        [SerializeField] private LayerMask _layer = ~0; 
        
        private Collider2D _collider2d;

        private void Awake()
        {
            _collider2d = GetComponent<Collider2D>();
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.IsInLayer(_layer)) return;
            if (!string.IsNullOrEmpty(_tag) && !other.gameObject.CompareTag(_tag)) return;
            
            _action?.Invoke(other.gameObject);
        }
    }
    
    [Serializable]
    public class EnterEvent : UnityEvent<GameObject>
    {

    }
}