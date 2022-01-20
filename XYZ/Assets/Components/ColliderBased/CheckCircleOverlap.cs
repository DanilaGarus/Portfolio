using System;
using System.Linq;
using Components.Hero_files;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Components.ColliderBased
{
    public class CheckCircleOverlap : MonoBehaviour
    {
        [SerializeField] private float _radius = 1f;
        [SerializeField] private OnOverlapEvent _OnOverlap;
        [SerializeField] private LayerMask _mask;
        [SerializeField] private string[] _tags;
        

        private Collider2D[] _interactionResult = new Collider2D[10];

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Handles.color = HandlessUtils.TransparentRed;
            Handles.DrawSolidDisc(transform.position, Vector3.forward, _radius);
        }
#endif
        

        public void Check()
        {
            var size = Physics2D.OverlapCircleNonAlloc(transform.position, _radius, _interactionResult, _mask);

           
            for (var i = 0; i < size; i++)
            {
                var overlapResult = _interactionResult[i];
                var isInTags = _tags.Any(tag => overlapResult.CompareTag(tag));
                if (isInTags)
                {
                    _OnOverlap.Invoke(overlapResult.gameObject);
                }               
            }
        }

        [Serializable]
        public class OnOverlapEvent : UnityEvent<GameObject>
        {
           
        }




    }
}