using System;
using Components.Model.Data.Properties;
using Components.Model.Definitions;
using Components.Model.Definitions.Repositories.Item;
using Components.Utils.Disposables;
using UnityEngine;

namespace Components.Model.Data
{
    public class QuickInventoryModel : IDisposable
    {
        private readonly PlayerData _playerData;

        public InventoryItemData[] Inventory { get; private set; }

        public readonly IntProperty SelectedIndex = new IntProperty();

        public event Action OnChanged;

        public InventoryItemData SelectedItem
        {
            get
            {
                if (Inventory.Length > 0 && Inventory.Length > SelectedIndex.Value)
                    return Inventory[SelectedIndex.Value];
                
                return null;
            }
        }

        public ItemDefenitions SelectedDef => DefsFacade.I.Items.Get(SelectedItem?._id);
        
        public QuickInventoryModel(PlayerData data)
        {
            _playerData = data;

            Inventory = _playerData.Inventory.GetAll(ItemTag.Usable);
            _playerData.Inventory.OnChange += OnChangedInventory;
        }

        public IDisposable Subscribe(Action call)
        {
            OnChanged += call;
            return new ActionDisposable(() => OnChanged -= call);
        }

        private void OnChangedInventory(string id, int value)
        {
            Inventory = _playerData.Inventory.GetAll(ItemTag.Usable);
            SelectedIndex.Value = Mathf.Clamp(SelectedIndex.Value, 0, Inventory.Length - 1); 
            OnChanged?.Invoke();
        }

        public void SetNextItem()
        {
            SelectedIndex.Value = (int) Mathf.Repeat(SelectedIndex.Value + 1, Inventory.Length);
        }

        public void Dispose()
        {
            _playerData.Inventory.OnChange -= OnChangedInventory;
        }
    }
}