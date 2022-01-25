using System;
using Components.Creatures.Hero;
using Components.Model;
using Components.Model.Data;
using Components.Model.Definitions;
using Components.Utils;
using TMPro;
using UnityEngine;

namespace Components.Collectables
{
    public class AddInInventoryComponent : MonoBehaviour
    {
        [InventoryId] [SerializeField] private string _id;
        [SerializeField] private int _count;

        public void Add(GameObject go)
        {
            var hero = go.GetInterface<ICanAddInInventory>();
            hero?.AddInInventory(_id,_count);
        }
    }
}