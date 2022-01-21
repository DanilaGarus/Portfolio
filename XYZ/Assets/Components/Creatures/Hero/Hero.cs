using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Creatures;
using Components.ColliderBased;
using Components.GameObjectBased;
using Components.Health;
using Components.Model;
using Components.Utils;
using Components.World_Scripts;
using UnityEngine;
using UnityEngine.Events;

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
        private int _swordCount;

        [Header("Burst Throw")] [SerializeField]
        private Cooldown _burstCooldown;
        [SerializeField] private int _burstParticles;
        [SerializeField] private float _burstDelay;
        private bool _burstThrow;
        
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
        
       [SerializeField] private ProbabilityDropComponent _hitDrop;    

        private GameSession _session;

        
        protected static readonly int ThrowKey = Animator.StringToHash("Throw");
        private readonly Collider2D[] _interactionResult = new Collider2D[1];        
        private bool _allowDoubleJump;

        [Space] [Header("Heal Event")]
        [SerializeField] private UnityEvent _action;
        
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
            
            _session.Data.Inventory.OnChange += OnInventoryChange;
            _session.Data.Inventory.OnChange += AnotherHandler;
            
            var health = GetComponent<HealthComponent>();
            health.SetHealth(_session.Data._hp);

            UpdateHeroWeapon();
        }

        private void OnDestroy()
        {
            _session.Data.Inventory.OnChange -= OnInventoryChange;
            _session.Data.Inventory.OnChange -= AnotherHandler;
        }

        private void AnotherHandler(string id, int value)
        {
            Debug.Log($"Inventory changed: {id}: {value}");
        }

        private void OnInventoryChange(string id, int value)
        {
            if(id == "Sword") UpdateHeroWeapon();
        }

        private void OnEnable()
        {
            ps = GetComponent<ParticleSystem>();
        }

        private void OnParticleTrigger()
        {
            var numCoins = _session.Data.Inventory.Count("Coins");
            int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

            for (int i = 0; i < numEnter; i++)
            {
                ParticleSystem.Particle p = enter[i];
                numCoins += 1;
                enter[i] = p;
                Debug.Log($" 1 coins added. Total count of coins: {numCoins}");

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

        public void AddInInventory(string id, int value)
        {
            _session.Data.Inventory.Add(id,value);
        }
        
        public override void TakeDamage()
        {
            base.TakeDamage();
            var numCoins = _session.Data.Inventory.Count("Coins");
            if (numCoins > 0)
            {
                SpawnCoins();
            }
        }
        
        private void SpawnCoins()
        {
            var numCoins = _session.Data.Inventory.Count("Coins");
            var _numCoinsDispose = Mathf.Min(numCoins, 5);
            _session.Data.Inventory.Remove("Coins", _numCoinsDispose);
          
            _hitDrop.SetCount(_numCoinsDispose);
            _hitDrop.CalculateDrop();
        }

        public void Interact()
        {
            _interactionCheck.Check();           
        }

        public void Heal()
        {
            if (_session.Data.Inventory.Count("Heal") > 0)
            {
                _session.Data.Inventory.Remove("Heal",1);
                _action?.Invoke();
            }
        }
        
        public override void Attack()
        {
            if (_attackCoolDown.IsReady)
            {
                if (_session.Data.Inventory.Count("Sword") <= 0) return;
                base.Attack();      
                _attackCoolDown.Reset();
            }
        }

        public void OnDoThrow()
        {
            if (_burstThrow)
            {
                var throwsCount = Mathf.Min(_burstParticles, _session.Data.Inventory.Count("Sword") - 1);
                StartCoroutine(DoBurst(throwsCount));
            }
            else
            {
                BurstAndRemoveFromInventory();
            }

            _burstThrow = false;
        }

        private IEnumerator DoBurst(int throwsCount)
        {
            for (int i = 0; i < throwsCount; i++)
            {
                BurstAndRemoveFromInventory();
                yield return new WaitForSeconds(_burstDelay);
            }
        }


        private void BurstAndRemoveFromInventory()
        {
            _particles.Spawn("Throw");
            _session.Data.Inventory.Remove("Sword",1);
        }
        
        public void StartBurst()
        {
            _burstCooldown.Reset();
        }

        public void PerformBurst()
        {
            var numSwords = _session.Data.Inventory.Count("Sword");
            if (!_throwCoolDown.IsReady ||  numSwords <= 1) return;

            if (_burstCooldown.IsReady) _burstThrow = true;
            
            Animator.SetTrigger(ThrowKey);  
            _throwCoolDown.Reset();
        }
        
        public void AddSwords()
        {
            var numSwords = _session.Data.Inventory.Count("Sword");
            numSwords++;
        }

        public void ArmHero()
        {
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
            Animator.runtimeAnimatorController = _session.Data.Inventory.Count("Sword") > 0 ? _armed : _disarmed;
        }
    }
}
  




