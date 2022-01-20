using System;
using System.Collections.Generic;
using Components.Model.Definitions;
using UnityEditor.Timeline.Actions;
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
          
              var item = GetItem(id);
            if (item == null)
            {
                item = new InventoryItemData(id);
                _inventory.Add(item);
            }
            
            item._value += value;
            
            OnChange?.Invoke(id,Count(id));
        }

        public void Remove(string id, int value)
        {
            var itemDef =  DefsFacade.I.Items.Get(id);
            if (itemDef.IsVoid) return;
            
            var item = GetItem(id);
            if(item == null) return;
            
            item._value -= value;

            if (item._value <= 0)
            {
                _inventory.Remove(item);
            }
            OnChange?.Invoke(id,Count(id));
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