using Components.Model.Data;
using Components.Model.Definitions.Repositories;
using Components.Model.Definitions.Repositories.Item;
using UnityEngine;

namespace Components.Model.Definitions
{
    [CreateAssetMenu(menuName = "Defs/DefsFacade", fileName = "DefsFacade" )]
    public class DefsFacade : ScriptableObject
    {
        [SerializeField] private ItemsRepository _items;
        [SerializeField] private ThrowableRepository _throwableItems;
        [SerializeField] private PlayerDef _playerDef;
        [SerializeField] private PotionRepository _potionRep;

        
        public ItemsRepository Items => _items;
        
        public ThrowableRepository Throwable => _throwableItems;
        public PlayerDef PlayerDef => _playerDef;
        
        public PotionRepository PotionRep => _potionRep;
        
        private static DefsFacade _instance;
        public static DefsFacade I => _instance == null ? LoadDefs() : _instance;

        private static DefsFacade LoadDefs()
        {
            return _instance = Resources.Load<DefsFacade>("DefsFacade");
        }
    }
}