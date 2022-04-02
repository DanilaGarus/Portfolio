using System;
using Components.Model.Definitions.Repositories.Item;
using UnityEngine;

namespace Components.Model.Definitions.Repositories
{  
    [CreateAssetMenu(menuName = "Defs/Throwable", fileName = "Throwable" )]

    public class ThrowableRepository : DefRepository<ThrowableDef>
    {
       
    }

    [Serializable]
    public struct ThrowableDef : IHaveId
    {
        [InventoryId] [SerializeField] private string _id;
        [SerializeField] private GameObject _projectile;

        public string Id => _id;
        public GameObject Projectile => _projectile;
    }
}