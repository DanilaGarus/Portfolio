using System;
using Components.Model.Data.Properties;
using Components.Utils.Disposables;
using UnityEngine;
using UnityEngine.UI;

namespace Components.UI.Widgets
{
    public class AudioSettingWidjet : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Text _value;

        private FloatPersistentProperty _model;

        private CompositeDisposable _trash = new CompositeDisposable();
        
        private void Start()
        {
            _trash.Retain(_slider.onValueChanged.Subscribe(OnSliderValueChanged));
        }

        public void SetModeel(FloatPersistentProperty model)
        {
            _model = model;
            _trash.Retain(model.Subscribe(OnValueChanged));
            OnValueChanged(model.Value, model.Value);
        }
        
        private void OnSliderValueChanged(float value)
        {
            _model.Value = value;
        }
        
        private void OnValueChanged(float newValue, float oldValue)
        {
            var textValue = Mathf.Round(newValue * 100);
            _value.text = textValue.ToString();
            _slider.normalizedValue = newValue;
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}