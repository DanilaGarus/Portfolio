using Components.Audio;
using UnityEngine;
using Components.ColliderBased;
using Components.GameObjectBased;

namespace Assets.Creatures
{
    public class Creature : MonoBehaviour
    {
        [Header("Params")]
        [SerializeField] protected bool _invertScale;
        [SerializeField] protected float _speed;
        [SerializeField] protected float _jumpspeed;
        [SerializeField] protected float _DamageVelocity;
        
        [Header("Chekers")]
        [SerializeField] protected LayerMask _groundlayer;
        [SerializeField] protected LayerMask _BarrelLayer;
        [SerializeField] protected ColliderCheck _GroundCheck;
        [SerializeField] protected ColliderCheck _BarrelCheck;
        [SerializeField] private CheckCircleOverlap _attackRange;
        [SerializeField] protected SpawnListComponent _particles;

        protected Rigidbody2D Rigidbody;
        protected Animator Animator;
        protected PlaySoundsComponent Sounds;
        protected Vector2 Direction;      
        protected bool IsGrounded;
        protected bool IsJumping;

        protected static readonly int IsGround = Animator.StringToHash("Is_Grounded");
        protected static readonly int IsRunning = Animator.StringToHash("Is_Running");
        protected static readonly int VerticalVelocity = Animator.StringToHash("vertical_velocity");
        protected static readonly int Hit = Animator.StringToHash("Hit_trigger");
        protected static readonly int Attackk = Animator.StringToHash("Attack");
        
        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            Sounds = GetComponent<PlaySoundsComponent>();
        }

        public void SetDirection(Vector2 direction)
        {
            Direction = direction;
        }
        
        protected virtual void Update()
        {
            IsGrounded = _GroundCheck.IsTouchingLayer;
        }

        protected virtual void FixedUpdate()
        {
            var xVelocity = CalculateXVelocity(); 
            var yVelocity = CalculateYVelocity();

            Rigidbody.velocity = new Vector2(xVelocity, yVelocity);
            
            Animator.SetBool(IsRunning, Direction.x != 0);
            Animator.SetFloat(VerticalVelocity, Rigidbody.velocity.y);
            Animator.SetBool(IsGround, IsGrounded);
            
            UpdateSpriteDirection(Direction);
        }

        protected virtual float CalculateXVelocity()
        {
          return  Direction.x * CalculateSpeed();
        }

        protected virtual float CalculateSpeed()
        {
            return _speed;
        }

        protected virtual float CalculateYVelocity()
        {
             var yVelocity = Rigidbody.velocity.y;
            var IsJumpPressing = Direction.y > 0;

            if (IsGrounded) IsJumping = false;
            
            if (IsJumpPressing)
            {
                IsJumping = true;

                var isFalling = Rigidbody.velocity.y <= 0.001f;
                if (!isFalling) return yVelocity;
                yVelocity = isFalling ? CalculateJumpVelocity(yVelocity) : yVelocity;

            }
            else if (Rigidbody.velocity.y > 0 && IsJumping)
            {
                yVelocity *= 0.5f;
            }
            return yVelocity;
        }

        protected virtual float CalculateJumpVelocity(float yVelocity)
        { 
            if (IsGrounded)
            {
                yVelocity += _jumpspeed;
                DoJumpVfx();
            }
            return yVelocity;
        }

        protected void DoJumpVfx()
        {
            _particles.Spawn("Jump");   
            Sounds.Play("Jump");
        }
        
        public void UpdateSpriteDirection(Vector2 direction)
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

        public virtual void TakeDamage()
        {
            IsJumping = false;
            Animator.SetTrigger(Hit);
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, _DamageVelocity);            
        }

        public virtual void Attack()
        {            
            Animator.SetTrigger(Attackk);
            Sounds.Play("Melee");
        }

        public void OnDoAttack()
        {
            _attackRange.Check();
            _particles.Spawn("Attack");
        }
    }
}