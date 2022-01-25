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
        [SerializeField] private float _alarmDelay = 1f;
        [SerializeField] private float _missHeroCooldown = 0.5f;
        
        [SerializeField] private ColliderCheck _vision;
        [SerializeField] private SpriteAnimation _sprite;
        private GameObject _target;
        private PinkStar _pinkStar;
        private Coroutine _current;
        private SpawnListComponent _particles;

        private void Awake()
        {
            _pinkStar = GetComponent<PinkStar>();
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
            // _particles.Spawn("Exclamation");
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
            if (_vision.IsTouchingLayer) 
            { 
                _sprite.SetClip("Attack-spin"); 
                SetDirectionToTarget(); 
                yield return null; 
                
            }
            else if (!_vision.IsTouchingLayer)
            {
                _pinkStar.SetDir(Vector2.zero); 
                yield return new WaitForSeconds(_missHeroCooldown); 
                _sprite.SetClip("idle"); 
                LookAtHero();
            }
            //_particles.Spawn("Miss");
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