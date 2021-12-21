using System.Collections.Generic;
using Assets.Creatures;
using Components.ColliderBased;
using Components.Health;
using Components.Model;
using Components.Utils;
using UnityEngine;
//using UnityEditor.Animations;


namespace Components.Creatures.Hero
{
    public class Hero : Creature
    {
        [Space]
        [Header("Values")]    
        [SerializeField] private float _fallVelocity;        
        [SerializeField] private Vector3 _groundCheckPositionDelta;
        [SerializeField] private float _groundCheckRadius;
        [SerializeField] private bool _isArmed;
        public float _swordCount;

        [Space] [Header("Other")] 
        [SerializeField] private Cooldown _throwCoolDown;

        [SerializeField] private Cooldown _attackCoolDown;
        
        [SerializeField] private float _interactionRadius;        
        [SerializeField] private RuntimeAnimatorController _armed;
        [SerializeField] private RuntimeAnimatorController _disarmed;
        [SerializeField] private CheckCircleOverlap _interactionCheck;

        // Для билда убирать using UnityEditor.Animations и менять 
        // AnimationContrller на RuntimeAnimatorController
        //или добавить #IF #UNITY;
        
        [Space]
        [Header("Particles")]
        [SerializeField] private ParticleSystem _hitParticles;       

        private GameSession _session;

        
        protected static readonly int ThrowKey = Animator.StringToHash("Throw");
        private readonly Collider2D[] _interactionResult = new Collider2D[1];        
        private bool _allowDoubleJump;


        ParticleSystem ps;

        private List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();


        private Collider2D _collider2d;

        protected override void Awake()
        {
            base.Awake();
            _collider2d = GetComponent<Collider2D>();
        }  

        private void Start()
        {    
            _session = FindObjectOfType<GameSession>();
            
            _session.Save();
            
            var health = GetComponent<HealthComponent>();
            health.SetHealth(_session.Data._hp);
            UpdateHeroWeapon();
        }

        private void OnEnable()
        {
            ps = GetComponent<ParticleSystem>();
        }

        private void OnParticleTrigger()
        {
            int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

            for (int i = 0; i < numEnter; i++)
            {
                ParticleSystem.Particle p = enter[i];
                _session.Data._coins += 1;
                enter[i] = p;
                Debug.Log($" 1 coins added. Total count of coins: {_session.Data._coins}");

            }
           
            ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        }

        public void OnHealthChanged(int currentHealth)
        {
            _session.Data._hp = currentHealth;
        }

        protected override void Update()
        {
            base.Update();            
        }              
        
        protected override float CalculateYVelocity()
        {            
            var IsJumpPressing = Direction.y > 0;

            if (IsGrounded) _allowDoubleJump = true; IsJumping = false;            

            return base.CalculateYVelocity();
        }

        protected override float CalculateJumpVelocity(float yVelocity)
        {
             if (!IsGrounded && _allowDoubleJump)
             {
                _particles.Spawn("Jump");                
                _allowDoubleJump = false;
                return _jumpspeed;
             }
             return base.CalculateJumpVelocity(yVelocity);
        }   

        public void AddCoins(int coins)
        {
            _session.Data._coins += coins;
            Debug.Log($"{coins} coins added. Total count of coins: {_session.Data._coins}");
        }

        public override void TakeDamage()
        {
            base.TakeDamage();

            if (_session.Data._coins > 0)
            {
                SpawnCoins();
            }
        }

        public void SpikesHeal()
        {
            Animator.SetTrigger(Heal);
        }
        private void SpawnCoins()
        {
            var _numCoinsDispose = Mathf.Min(_session.Data._coins, 5);
            _session.Data._coins -= _numCoinsDispose;

            var burst = _hitParticles.emission.GetBurst(0);
            burst.count = _numCoinsDispose;
            _hitParticles.emission.SetBurst(0, burst);
            _hitParticles.gameObject.SetActive(true);
            _hitParticles.Play();
        }

        public void Interact()
        {
            _interactionCheck.Check();           
        }

        public override void Attack()
        {
            if (_attackCoolDown.IsReady)
            {
                if (!_session.Data.IsArmed) return;
                base.Attack();      
                _attackCoolDown.Reset();
            }
                  
        }

        public void OnDoThrow()
        {
            _particles.Spawn("Throw");
        }

        public void Throw()
        {
            if (_throwCoolDown.IsReady && _swordCount > 1)
            {
                Animator.SetTrigger(ThrowKey);  
                _throwCoolDown.Reset();
                _swordCount--;
            }
            
            if (_swordCount <= 1)
            {
                return;
            }
        }

        public void AddSwords()
        {
            _swordCount++;
        }

        public void ArmHero()
        {
            _session.Data.IsArmed = true;
            UpdateHeroWeapon();
            Animator.runtimeAnimatorController = _armed;
            AddSwords();
        }        

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.IsInLayer(_groundlayer))
            {
                var contact = other.contacts[0];
                if (contact.relativeVelocity.y >= _fallVelocity)
                {
                    _particles.Spawn("Fall");
                    
                }
            }
        }

        private void UpdateHeroWeapon()
        {
            Animator.runtimeAnimatorController = _session.Data.IsArmed ? _armed : _disarmed;

            //if (_session.Data.IsArmed)
            //{
            //    _animator.runtimeAnimatorController = _armed;
            //}
            //else
            //{
            //    _animator.runtimeAnimatorController = _disarmed;
            //}
        }
    }
}
  




