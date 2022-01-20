using Components.ColliderBased;
using UnityEngine;

public class ColliderCheck : LayerCheck
{
    
    
    private Collider2D _collider2d;
        
    private void Awake()
    {
        _collider2d = GetComponent<Collider2D>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        _isTouchingLayer  = _collider2d.IsTouchingLayers(_Layer ^ _BarrelLayer);

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _isTouchingLayer = _collider2d.IsTouchingLayers(_Layer ^ _BarrelLayer);
      
    }
    
}

