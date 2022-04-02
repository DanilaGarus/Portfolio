using System;
using System.Collections.Generic;
using System.Linq;
using Components.Model.Definitions;
using Components.Model.Definitions.Repositories.Item;
using UnityEngine;

namespace Components.Model.Data
{
    [Serializable]
    public class InventoryData
    {
        [SerializeField] private List<InventoryItemData> _inventory = new List<InventoryItemData>();

        public delegate void OnInventoryChange(string id, int value);

        public OnInventoryChange OnChange;
        public void Add(string id, int value)
        {
            if(value <= 0) return;
            
            var itemDef =  DefsFacade.I.Items.Get(id); 
            if (itemDef.IsVoid) return;

            var isFull = _inventory.Count >= DefsFacade.I.PlayerDef.InventorySize;
            if (itemDef.HasTag(ItemTag.Stackable))
            { 
                AddToStack(id, value);
            }
            else
            { 
                AddNonStack(id, value);
            }
            
            OnChange?.Invoke(id,Count(id));
        }

        public InventoryItemData[] GetAll(params ItemTag[] tags)
        {
            var retValue = new List<InventoryItemData>();
            foreach (var item in _inventory)
            {
                var itemDef = DefsFacade.I.Items.Get(item._id);
                var isAllRequirementsMet = tags.All(x => itemDef.HasTag(x));
                if(isAllRequirementsMet)
                    retValue.Add(item);
            }
            
            return retValue.ToArray();
        }
        
        private void AddToStack(string id, int value)
        {
            var isFull = _inventory.Count >= DefsFacade.I.PlayerDef.InventorySize;
            var item = GetItem(id);
            if (item == null)
            {
                if(isFull) return;
                item = new InventoryItemData(id);
                _inventory.Add(item);
            }
            item._value += value;
        }
        
        private void AddNonStack(string id, int value)
        {
            var itemInInventory = DefsFacade.I.PlayerDef.InventorySize - _inventory.Count;
            value = Mathf.Min(itemInInventory, value);
            
            for (int i = 0; i < value; i++)
            {
                var item = new InventoryItemData(id) {_value = 1};
                _inventory.Add(item);
            }
        }

        public void Remove(string id, int value)
        {
            var itemDef =  DefsFacade.I.Items.Get(id);
            if (itemDef.IsVoid) return;

            if (itemDef.HasTag(ItemTag.Stackable))
            { 
                RemoveFromStack(id, value);
            }
            else
            {
                RemoveNonStack(id, value);
            }
           
            OnChange?.Invoke(id,Count(id));
        }

        private void RemoveFromStack(string id, int value)
        {
            var item = GetItem(id);
            if(item == null) return;
            
            item._value -= value;

            if (item._value <= 0)
            {
                _inventory.Remove(item);
            }
        }

        private void RemoveNonStack(string id, int value)
        {
            for (int i = 0; i < value; i++)
            {
                var item = GetItem(id);
                if(item == null) return;

                _inventory.Remove(item);
            }
        }
        
        private InventoryItemData GetItem(string id)
        {
            foreach (var itemData in _inventory)
            {
                if (itemData._id == id) 
                    return itemData;
            }

            return null;
        }

        public int Count(string id)
        {
            var count = 0;
            foreach (var item in _inventory)
            {
                if (item._id == id)
                    count += item._value;
            }

            return count;
        }
    }
    
    [Serializable]
    public class InventoryItemData
    { 
        [InventoryId] public string _id;
        public int _value;

        public InventoryItemData(string id)
        {
            _id = id;
        }
    }
}