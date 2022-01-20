using Components.Model.Data;
using UnityEngine;

namespace Components.Model.Definitions
{
    [CreateAssetMenu(menuName = "Defs/DefsFacade", fileName = "DefsFacade" )]
    public class DefsFacade : ScriptableObject
    {
        [SerializeField] private InventoryItemsDefenitions _items;

        public InventoryItemsDefenitions Items => _items;
        
        private static DefsFacade _instance;
        public static DefsFacade I => _instance == null ? LoadDefs() : _instance;

        private static DefsFacade LoadDefs()
        {
            return _instance = Resources.Load<DefsFacade>("DefsFacade");
        }
    }
}