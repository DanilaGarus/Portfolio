using System;
using System.Linq;
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
        [SerializeField] private Sprite _icon;
        [SerializeField] private ItemTag[] _tags;
        
        public string ID => _id;
        public bool IsVoid => string.IsNullOrWhiteSpace(_id);
        public Sprite Icon => _icon;

        public bool HasTag(ItemTag tag)
        {
            return _tags.Contains(tag);
        }
        
    }
}