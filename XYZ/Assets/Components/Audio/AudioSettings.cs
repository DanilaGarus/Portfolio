using System;
using Components.Model.Data;
using Components.Model.Data.Properties;
using UnityEngine;

namespace Components.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSettings : MonoBehaviour
    {
        [SerializeField] private GameSettings.SoundSettings _mode;
        private FloatPersistentProperty _model;
        private AudioSource _source;
        
        private void Start()
        {
            _source = GetComponent<AudioSource>();
            
             _model = FindProperty();
             _model.OnChanged += OnSoundSettingsChanged;
             OnSoundSettingsChanged(_model.Value, _model.Value);
        }

        private void OnSoundSettingsChanged(float newvalue, float oldvalue)
        {
            _source.volume = newvalue;
        }

        private FloatPersistentProperty FindProperty()
        {
            switch (_mode)
            {
                case GameSettings.SoundSettings.Music:
                    return GameSettings.I.Music;
                    
                case GameSettings.SoundSettings.Sfx:
                    return GameSettings.I.Sfx;
            }

            throw new ArgumentException("Undefined mode");
        }

        private void OnDestroy()
        {
            _model.OnChanged -= OnSoundSettingsChanged;
        }
    }
}