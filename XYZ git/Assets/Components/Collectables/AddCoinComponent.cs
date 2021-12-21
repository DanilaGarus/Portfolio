using Assets.Creatures;
using Components.Creatures.Hero;
using UnityEngine;

namespace Components.Collectables
{
    public class AddCoinComponent : MonoBehaviour
    {
        [SerializeField] private int _numCoins;
        private Hero _hero;

        public void Start()
        {
            _hero = FindObjectOfType<Hero>();
        }
    
        public void Add()
        {
            _hero.AddCoins(_numCoins);
        }
    }
}
