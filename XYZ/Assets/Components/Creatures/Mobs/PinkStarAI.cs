using System;
using System.Collections;
using Assets.Creatures;
using Components.Creatures.Mobs.Patroling;
using Components.GameObjectBased;
using Components.Model;
using Components.World_Scripts;
using JetBrains.Annotations;
using UnityEngine;

namespace Components.Creatures.Mobs
{
    public class PinkStarAI : MonoBehaviour
    {
        [Header("Params")]
        [SerializeField] private float _alarmDelay = 1f;
        [SerializeField] private float _missHeroCooldown = 0.5f;
        
        [Header("Chekers")]
        [SerializeField] private ColliderCheck _vision;
        [SerializeField] private SpriteAnimation _sprite;
        [SerializeField] private GameObject _attackCollider2D;
        
        private GameObject _target;
        private PinkStar _pinkStar;
        private Coroutine _current;
        private SpawnListComponent _particles;

        private void Awake()
        {
            _pinkStar = GetComponent<PinkStar>();
            _particles = GetComponent<SpawnListComponent>();
        }

        public void StartWait(GameObject target)
        {
            StartState(WaitForTrigger(target));
        }
        
        public IEnumerator WaitForTrigger(GameObject target)
        {
            _target = target;
            yield return new WaitForSeconds(_alarmDelay);
            StartState(ArgToHero(target));
             _particles.Spawn("Exclamination");
        }
        
        private IEnumerator ArgToHero(GameObject target)
        {
            LookAtHero();
            _sprite.SetClip("prepare-to-attack");
            yield return new WaitForSeconds(_alarmDelay);
            StartState(Attack());
        }

        private void LookAtHero()
        {
            var direction = GetDirectionToTarget();
            _pinkStar.SetDir(Vector2.zero);
            _pinkStar.UpdateSpriteDir(direction);
        }

        private IEnumerator Attack()
        {
            _attackCollider2D.SetActive(true);
            _sprite.SetClip("Attack-spin"); 
            while (_vision.IsTouchingLayer) 
            {
                SetDirectionToTarget(); 
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);
            _pinkStar.SetDir(Vector2.zero);
            _attackCollider2D.SetActive(false);
            _sprite.SetClip("idle");
            _particles.Spawn("Miss");
            yield return new WaitForSeconds(_missHeroCooldown);
            LookAtHero();
            
            if (!_vision.IsTouchingLayer)
            {
                LookAtHero();
            }
        }
        
        private void SetDirectionToTarget()
        {
            var direction = GetDirectionToTarget();
            _pinkStar.SetDir(direction);
        }
        
        private Vector2 GetDirectionToTarget()
        {
            var direction = _target.transform.position - transform.position;
            direction.y = 0;
            return direction.normalized;
        }
        
        private void StartState(IEnumerator coroutine)
        { 
            _pinkStar.SetDir(Vector2.zero); 
            if (_current != null) StopCoroutine(_current);
            
            _current = StartCoroutine(coroutine);    
        }
    }
}