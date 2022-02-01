using System;
using UnityEngine;
using UnityEngine.UI;

namespace Components.UI.MainMenu
{
    public class MenuTimeComponent : MonoBehaviour
    {
        private Text _canvas;
        private void Start()
        {
            Time.fixedDeltaTime = 20f;
            _canvas = GetComponent<Text>();
        }

        private void FixedUpdate()
        { 
            _canvas.text = DateTime.Now.ToShortTimeString();
        }
    }
}