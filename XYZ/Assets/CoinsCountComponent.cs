using System;
using System.Collections;
using System.Collections.Generic;
using Components.Creatures.Hero;
using Components.Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CoinsCountComponent : MonoBehaviour
{
   [SerializeField] private Text _coinsCount;
   private GameSession _session;
   private GameObject _hud;
   private int count => _session.Data.Inventory.Count("Coins");
   
   private void Awake()
   {
       _session = FindObjectOfType<GameSession>();
       
   }

   private void Start()
   {
       _coinsCount.text = "You collected " + count + " coins !";
   }

   private void Update()
    {
        try
        {
            _hud = GameObject.FindWithTag("PauseCanvas");
            Destroy(_hud);  
        }
        catch 
        {
            return;
        } 
    }

   public void MenuExit()
   {
       SceneManager.LoadScene("MainMenu");
       var session = FindObjectOfType<GameSession>();
       Destroy(session.gameObject);
   }
}
