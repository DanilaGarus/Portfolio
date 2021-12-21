using UnityEngine;

namespace Components.World_Scripts
{
    public class TeleportComponent : MonoBehaviour
    {
        [SerializeField] private Transform _PointToTeleport;

        public void Teleport(GameObject target)
        {
            target.transform.position = _PointToTeleport.position;
        }


    }
}

