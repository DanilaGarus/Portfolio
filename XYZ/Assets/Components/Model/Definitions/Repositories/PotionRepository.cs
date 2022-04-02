using System;
using Components.Model.Definitions.Repositories.Item;
using TMPro;
using UnityEngine;

namespace Components.Model.Definitions.Repositories
{   
    [CreateAssetMenu(menuName = "Defs/Potions", fileName = "Potions" )]

    public class PotionRepository : DefRepository<PoitionDef>
    {
        
    }

    [Serializable]
    public struct PoitionDef : IHaveId
    {
        [InventoryId] [SerializeField] private string _id;
        [SerializeField] private Effect _effect;
        [SerializeField] private float _value;
        [SerializeField] private float _time;
        public string Id => _id;
        public Effect Effect => _effect;
        public float Value => _value;

        public float Time => _time;
    }

    public enum Effect
    {
        AddHp,
        SpeedUp
    }
}