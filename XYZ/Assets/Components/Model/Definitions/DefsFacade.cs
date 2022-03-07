using Components.Model.Data;
using UnityEngine;

namespace Components.Model.Definitions
{
    [CreateAssetMenu(menuName = "Defs/DefsFacade", fileName = "DefsFacade" )]
    public class DefsFacade : ScriptableObject
    {
        [SerializeField] private InventoryItemsDefenitions _items;
        [SerializeField] private ThrowableItemDef _throwableItems;
        [SerializeField] private PlayerDef _playerDef;

        public InventoryItemsDefenitions Items => _items;
        
        public ThrowableItemDef Throwable => _throwableItems;
        public PlayerDef PlayerDef => _playerDef;
        
        private static DefsFacade _instance;
        public static DefsFacade I => _instance == null ? LoadDefs() : _instance;

        private static DefsFacade LoadDefs()
        {
            return _instance = Resources.Load<DefsFacade>("DefsFacade");
        }
    }
}