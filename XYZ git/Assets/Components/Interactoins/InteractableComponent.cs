using UnityEngine;
using UnityEngine.Events;

namespace Components.Interactoins
{
    public class InteractableComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent _action;


        public void Interact()
        {
            _action?.Invoke();
        }


    }
}

