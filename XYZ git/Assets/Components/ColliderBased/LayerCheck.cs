using UnityEngine;

namespace Components.ColliderBased
{
    public class LayerCheck : MonoBehaviour
    {
        [SerializeField] protected LayerMask _Layer;
        [SerializeField] protected LayerMask _BarrelLayer;
        [SerializeField] protected bool _isTouchingLayer;
    
        public bool IsTouchingLayer => _isTouchingLayer;
    }
}