
using UnityEngine;

namespace Components.Creatures.Weapon
{
    public class SinusoidaProjectile : BaseProjectile
    {
        [SerializeField] private float _frequency = 1f;
        [SerializeField] private float _amplitude = 1f;
        
        private float _originalY;
        private float _time;
        
        protected override void Start()
        {
            base.Start();
            _originalY = _rigidbody.position.y;
        }
        
        private void FixedUpdate()
        {
            var pos = _rigidbody.position;
            pos.x += _direction * _speed;
            pos.y =_originalY + Mathf.Sin( + _time * _frequency) * _amplitude;
            _rigidbody.MovePosition(pos);
            _time += Time.fixedDeltaTime;
        }
    }
}