using System;
using UnityEngine;

namespace Components.Creatures.Weapon
{
    public class Projectile : BaseProjectile
    {
        protected override void Start()
        {
            base.Start();
        }

        private void FixedUpdate()
        {
            var position = _rigidbody.position;
            position.x += _direction * _speed;
            _rigidbody.MovePosition(position);
        }
    }
}