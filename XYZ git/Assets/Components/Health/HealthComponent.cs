using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Components.Health
{
    public class HealthComponent : MonoBehaviour
    {
        [Range(0, 50)] [SerializeField] private int _health;
        [SerializeField] private UnityEvent _onDamageTake;
        [SerializeField] private UnityEvent _onPoisionDamageTake;
        [SerializeField] private UnityEvent _poisionCollisionVelocity;
        [SerializeField] private UnityEvent _onDie;
        [SerializeField] private UnityEvent _onCut;
        [SerializeField] private HealthChangeEvent _onChange;
        private Coroutine _routine;


       private int dmg;


       

       public void ApplyDamage(int damageValue)
        {
            _health -= damageValue;
            _onChange?.Invoke(_health);
            _onDamageTake?.Invoke();
            if (_health <= 0)
            {
                _onDie.Invoke();
            }
            
            if (_health <= -3) 
            { 
                _onCut.Invoke();
            }
        }

        public void Heal(int heal)
        {
            _health += heal;
            _onChange?.Invoke(_health);
            StopCoroutine(_routine);
        }
                
        public void PeriodicDamage()
        {
            StopAllCoroutines();
            _health -= 1;
            _onChange?.Invoke(_health);
            _poisionCollisionVelocity.Invoke();
           _routine = StartCoroutine(OnPoisionDamage());
        }

        public IEnumerator OnPoisionDamage()
        {
            dmg = 0;

            while (dmg < 4)
            {
                yield return new WaitForSecondsRealtime(2);
                if (_health <= 0)
                {
                    _onDie.Invoke();
                }
                _health -= 1;
                _onChange?.Invoke(_health);
                _onPoisionDamageTake.Invoke();
                dmg++;                
            }          
        }

        
        
        internal void SetHealth(int health)
        {
            _health = health;
        }
    }

    [Serializable]
    public class HealthChangeEvent : UnityEvent<int>
    {

    }
}

