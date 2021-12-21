using System.Collections;
using Assets.Creatures;
using Components.Creatures.Mobs.Patroling;
using Components.GameObjectBased;
using UnityEngine;

namespace Components.Creatures.Mobs
{
    public class MobAI : MonoBehaviour
    {
        [SerializeField] private LayerCheck _vision;
        [SerializeField] private LayerCheck _canAttack;


        [SerializeField] private float _alarmDelay = 0.5f;
        [SerializeField] private float _attackCoolDown = 1f;
        [SerializeField] private float _missHeroCooldown = 2f;

        private Coroutine _current;
        private GameObject _target;
        private Creature _creature;
        private Animator _animator;
        private bool _isDead;
        private Patrol _patrol;

        private SpawnListComponent _particles;

        protected static readonly int isDeadKey = Animator.StringToHash("Is_Dead");

        private void Awake()
        {
            _particles = GetComponent<SpawnListComponent>();
            _creature = GetComponent<Creature>();
            _animator = GetComponent<Animator>();
            _patrol = GetComponent<Patrol>();
        }

        private void Start()
        {
            StartState(_patrol.DoPatrol());
        }        

        public void OnHeroInVision(GameObject target)
        {
            if (_isDead) return;
            
            _target = target;

            StartState(AgroToHero());
        }

        private IEnumerator AgroToHero()
        {
            LookAtHero();
            _particles.Spawn("Exclamation");
            yield return new WaitForSeconds(_alarmDelay);

            StartState(GoToHero());
        }

        private void LookAtHero()
        {
            var direction = GetDirectionToTarget();
            _creature.SetDirection(Vector2.zero);
            _creature.UpdateSpriteDirection(direction);
        }

        private IEnumerator GoToHero()
        {
            while (_vision.IsTouchingLayer)
            {
                if (_canAttack.IsTouchingLayer)
                {
                    StartState(Attack());
                }
                else
                {
                    SetDirectionToTarget();
                }
                
                yield return null;
            }
            _creature.SetDirection(Vector2.zero);
            _particles.Spawn("Miss");
            yield return new WaitForSeconds(_missHeroCooldown);
            StartState(_patrol.DoPatrol());
        }

        private IEnumerator Attack()
        {
            while (_canAttack.IsTouchingLayer)
            {
                _creature.Attack();

                yield return new WaitForSeconds(_attackCoolDown);
            }

            StartState(GoToHero());
        }

        private void SetDirectionToTarget()
        {

            var direction = GetDirectionToTarget();
            _creature.SetDirection(direction);
        }

        private Vector2 GetDirectionToTarget()
        {
            var direction = _target.transform.position - transform.position;
            direction.y = 0;
            return direction.normalized;
        }
        
        
        
        private void StartState(IEnumerator coroutine)
        {
            _creature.SetDirection(Vector2.zero);

            if (_current != null) StopCoroutine(_current);
            
            _current = StartCoroutine(coroutine);    
        }

        public void OnDie()
        {
            _isDead = true; 
                        
            _animator.SetBool(isDeadKey, true);
            
            _creature.SetDirection(Vector2.zero);

            if (_current != null) StopCoroutine(_current);
        }

    }
}