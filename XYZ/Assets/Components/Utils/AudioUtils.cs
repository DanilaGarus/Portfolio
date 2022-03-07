using UnityEngine;

namespace Components.Utils
{
    public class AudioUtils
    {
        public const string SfxSourceTag = "VFXSoundEffects";
        
        public static AudioSource FindSfxSource()
        {
           return GameObject.FindWithTag(SfxSourceTag).GetComponent<AudioSource>();
        }
    }
}