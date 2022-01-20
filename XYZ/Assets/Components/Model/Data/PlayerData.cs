using System;
using UnityEngine;

namespace Components.Model.Data
{

    [Serializable]
    public class PlayerData
    {
        [SerializeField] private InventoryData _inventoryData;
        public int _hp;

        public InventoryData Inventory => _inventoryData;
       
        public PlayerData Clone()
        {
            var json = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<PlayerData>(json);
        }
    }
}