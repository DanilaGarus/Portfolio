using UnityEngine;

namespace Components.ColliderBased
{
    public class LineCastCheck : LayerCheck
    {
        [SerializeField] private Transform _target;

        private RaycastHit2D[] _result = new RaycastHit2D[1];
        private void Update()
        {
            _isTouchingLayer = Physics2D.LinecastNonAlloc(transform.position, _target.position, _result, _Layer) > 0;
        }
        
        #if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            UnityEditor.Handles.DrawLine(transform.position, _target.position);
        }
        #endif
    }
}