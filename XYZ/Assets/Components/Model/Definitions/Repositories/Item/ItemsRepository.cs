using System;
using System.Linq;
using UnityEngine;

namespace Components.Model.Definitions.Repositories.Item
{
    [CreateAssetMenu(menuName = "Defs/Items", fileName = "Items" )]
    public class ItemsRepository : DefRepository<ItemDefenitions>
    {
        
#if UNITY_EDITOR
        public ItemDefenitions[] ItemForEditor => _collection;
#endif

    }

    [Serializable]
    public struct ItemDefenitions : IHaveId
    {
        [SerializeField] private string _id;
        [SerializeField] private Sprite _icon;
        [SerializeField] private ItemTag[] _tags;

        public string Id => _id;
        public bool IsVoid => string.IsNullOrWhiteSpace(_id);
        public Sprite Icon => _icon;

        public bool HasTag(ItemTag tag)
        {
            return _tags?.Contains(tag) ?? false;
        }
    }
}