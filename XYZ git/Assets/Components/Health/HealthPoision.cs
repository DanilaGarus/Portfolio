using UnityEngine;

namespace Components.Health
{
    public class HealthPoision : MonoBehaviour
    {
        [SerializeField] private int _heal;

        public void Heal(GameObject healing)
        {
            var HealthComponent = healing.GetComponent<HealthComponent>();

            if (HealthComponent != null)
            {
                HealthComponent.Heal(_heal);
            }
        }
    }
}

