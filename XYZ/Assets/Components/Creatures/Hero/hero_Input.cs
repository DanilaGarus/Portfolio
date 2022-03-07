using UnityEngine;
using UnityEngine.InputSystem;

namespace Components.Creatures.Hero
{
    public class hero_Input : MonoBehaviour
    {
        [SerializeField] private Hero _hero;

        public void OnHorizontalMovement(InputAction.CallbackContext context)
        {
            var derectionX = context.ReadValue<float>();
            _hero.SetDirection(new Vector2(derectionX,0));  
        }
        public void OnVerticalMovement(InputAction.CallbackContext context)
        {
            var directionY = context.ReadValue<float>();
            _hero.SetDirection(new Vector2(0,directionY));
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();
            _hero.SetDirection(direction);
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.Interact();
            }
        }

        public void OnNextItem(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.NextItem();
            }
        }
        
        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.Attack();
            }
        }

        public void OnFewThrow(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.OnDoThrow();
            }
        }

        public void OnThrow(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                _hero.StartBurst();
            }

            if (context.canceled)
            {
                _hero.PerformBurst();
            }
        }
    }
}

