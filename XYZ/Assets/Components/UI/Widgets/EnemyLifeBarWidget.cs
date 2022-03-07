using System;
using Components.Health;
using Components.Utils.Disposables;
using UnityEngine;

namespace Components.UI.Widgets
{
    public class EnemyLifeBarWidget : MonoBehaviour
    {
        [SerializeField] private ProgressBarWidget _lifeBar;
        [SerializeField] private HealthComponent _hp;
        [SerializeField] private GameObject _creature;
        [SerializeField] private bool _invertScale;

        private CompositeDisposable _trash = new CompositeDisposable();
        private int _maxHp;
        
        private void Start()
        {
            if (_hp == null)
                _hp = GetComponentInParent<HealthComponent>();

            _maxHp = _hp.Health;

            _trash .Retain(_hp._onDie.Subscribe(OnDie));
            _trash.Retain(_hp._onChange.Subscribe(OnHpChanged));
        }

        private void FixedUpdate()
        {
            var multiplier = _invertScale ? -0.03f : 0.03f;
            
            if(_creature.transform.localScale == new Vector3(1,1,1))
            {
                transform.localScale = new Vector3(multiplier, 0.03f, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1 * multiplier, 0.03f, 1);
            }
        }

        private void OnDie()
        {
            Destroy(gameObject);
        }

        private void OnHpChanged(int hp)
        {
            var progress = (float) hp / _maxHp;
            _lifeBar.SetProgress(progress);
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}