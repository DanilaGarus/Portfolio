using Components.Model;
using Components.Model.Data;
using Components.Model.Definitions;
using Components.Model.Definitions.Repositories.Item;
using Components.Utils.Disposables;
using UnityEngine;
using UnityEngine.UI;

namespace Components.UI.HUD.QuickInventory
{
    public class InventoryItemWidget : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _selection;
        [SerializeField] private Text _value;
        
        private readonly CompositeDisposable _trash = new CompositeDisposable();

        private int _index;

        private void Start()
        {
            var session = FindObjectOfType<GameSession>();
            var index = session.QuickInventory.SelectedIndex;
            _trash.Retain(index.SubscribeAndInvoke(OnIndexChanged));
        }

        private void OnIndexChanged(int newValue, int _)
        {
            _selection.SetActive(_index == newValue);
        }

        public void SetData(InventoryItemData item, int index)
        {
            _index = index;
             var def = DefsFacade.I.Items.Get(item._id);
            _icon.sprite = def.Icon;
            _value.text = def.HasTag(ItemTag.Stackable) ? item._value.ToString() : string.Empty;
        }
        
        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}