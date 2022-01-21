using System;
using UnityEngine;

namespace Components.Model.Definitions
{
    [CreateAssetMenu(menuName = "Defs/InventoryItems", fileName = "InventoryItems" )]
    public class InventoryItemsDefenitions : ScriptableObject
    {
        [SerializeField] public ItemDefenitions[] _items;

        public ItemDefenitions Get(string id)
        {
            foreach (var itemDefenitions in _items)
            {
                if (itemDefenitions.ID == id)
                    return itemDefenitions;
            }

            return default;
        }

#if UNITY_EDITOR
        public ItemDefenitions[] ItemForEditor => _items;
#endif

    }

    [Serializable]
    public struct ItemDefenitions
    {
        [SerializeField] private string _id;
        [SerializeField] private bool _canStack;
        public string ID => _id;
        public bool CanStack => _canStack;

        public bool IsVoid => string.IsNullOrWhiteSpace(_id);
    }
}