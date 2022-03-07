using System;
using Components.Model.Data.Properties;
using UnityEngine;

namespace Components.Model.Data
{

    [Serializable]
    public class PlayerData
    {
        [SerializeField] private InventoryData _inventoryData;
        
        public IntProperty _hp = new IntProperty();
        

        public InventoryData Inventory => _inventoryData;
       
        public PlayerData Clone()
        {
            var json = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<PlayerData>(json);
        }
    }
}