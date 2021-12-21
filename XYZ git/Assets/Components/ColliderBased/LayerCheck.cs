using UnityEngine;

public class LayerCheck : MonoBehaviour
{
    [SerializeField] private LayerMask _Layer;
    [SerializeField] private LayerMask _BarrelLayer;
    [SerializeField] private bool _isTouchingLayer;

    private Collider2D _collider2d;
    

    public bool IsTouchingLayer => _isTouchingLayer;
    


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

