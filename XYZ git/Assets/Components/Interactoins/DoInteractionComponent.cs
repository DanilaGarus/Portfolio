using UnityEngine;

namespace Components.Interactoins
{
    public class DoInteractionComponent : MonoBehaviour
    {
        public void doInteraction(GameObject go)
        {
            var interactable = go.GetComponent<InteractableComponent>();
            if (interactable != null)
                interactable.Interact();
        }
        
    }
}