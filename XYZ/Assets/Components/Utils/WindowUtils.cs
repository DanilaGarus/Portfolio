using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Components.Utils
{
    public static class WindowUtils
    {
        public static void CreateWindow(string resourcePath)
        {
            var window = Resources.Load<GameObject>(resourcePath);
            
            try
            {
                var canvas = GameObject.FindWithTag("PauseCanvas").GetComponent<Canvas>();
                Object.Instantiate(window, canvas.transform);
            }
            catch
            {
               var canvas = Object.FindObjectOfType<Canvas>();
               Object.Instantiate(window, canvas.transform);
            }
        }
    }
}