using UnityEngine;

namespace Components.Utils
{
    public static class NewMonoBehaviour1
    {
        public static bool IsInLayer(this GameObject go, LayerMask layer)
        {
            return layer == (layer | 1 << go.layer);
        }
       
    }
}