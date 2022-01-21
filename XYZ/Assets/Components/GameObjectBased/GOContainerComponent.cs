using Components.Model;
using Components.World_Scripts;
using UnityEngine;

namespace Components.GameObjectBased
{
    public class GOContainerComponent : MonoBehaviour
    {
        [SerializeField] private GameObject[] _gos;
        [SerializeField] private ProbabilityDropComponent.DropEvent _onDrop;

        [ContextMenu("Drop")]
        public void Drop()
        {
            _onDrop.Invoke(_gos);
        }
    }
}