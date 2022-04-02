using UnityEngine;
using UnityEngine.InputSystem;

namespace Components.LevelManagment
{
    public class ShowTargetComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private CameraStateController _controller;
        [SerializeField] private float _delay = 1f;
        [SerializeField] private PlayerInput _Input;

        private void OnValidate()
        {
            if (_controller == null)
                _controller = FindObjectOfType<CameraStateController>();
        }
        
        public void ShowTarget()
        {
            _controller.SetPosition(_target.position);
            _controller.SetState(true);
            _Input.enabled = false;
            Invoke(nameof(MoveBack), _delay);
        }

        private void MoveBack()
        {
            _controller.SetState(false);
            _Input.enabled = true;
        }
    }
}