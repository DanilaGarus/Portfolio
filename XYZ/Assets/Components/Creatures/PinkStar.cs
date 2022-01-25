using Components.Audio;
using Components.ColliderBased;
using UnityEngine;

namespace Assets.Creatures
{
    public class PinkStar : Creature
    {
        [SerializeField] private CheckCircleOverlap _attackRangePinkstar;
        private Rigidbody2D _rg;
        private Vector2 _direction;
        
        protected override void Awake()
        {
            _rg = GetComponent<Rigidbody2D>();
        }

        protected void Update()
        {
            
        }
        
        public void SetDir(Vector2 direction)
        {
            _direction = direction;
        }

        protected override void FixedUpdate()
        {
            var xVelocity = _direction.x * _speed;
            var yVelocity = _rg.velocity.y;

            _rg.velocity = new Vector2(xVelocity, yVelocity);
            
            UpdateSpriteDir(_direction);
        }
        
        public void UpdateSpriteDir(Vector2 direction)
        {
            var multiplier = _invertScale ? -1 : 1;

            if (direction.x > 0)
            {
                transform.localScale = new Vector3(multiplier, 1, 1); 
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1 * multiplier, 1, 1);
            }
        }

        public override void TakeDamage()
        {
            _rg.velocity = new Vector2(_rg.velocity.x,_DamageVelocity);
        }
    }
}