using System;
using Components.Creatures.Hero;
using Components.Model;
using Components.Model.Definitions;
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
            var hero = go.GetComponent<Hero>();
            if(hero != null) 
                hero.AddInInventory(_id,_count);
        }
    }
}