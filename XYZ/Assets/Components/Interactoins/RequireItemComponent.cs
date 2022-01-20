using Components.Model;
using Components.Model.Data;
using UnityEngine;
using UnityEngine.Events;

namespace Components.Interactoins
{
    public class RequireItemComponent : MonoBehaviour
    { 
        [SerializeField] private InventoryItemData[] _required; 
        [SerializeField] private bool _removeAfterUse;
       
       [SerializeField] private UnityEvent _onSucsess;
       [SerializeField] private UnityEvent _onFail;
       
       public void Check()
       {
           var session = FindObjectOfType<GameSession>();
           var areAllRequirementsMet = true;
           foreach (var item in _required)
           {
               var numItems =  session.Data.Inventory.Count(item._id);
               if (numItems < item._value)
                   areAllRequirementsMet = false;
           }
           
           if (areAllRequirementsMet)
           {
               if (_removeAfterUse)
               {
                   foreach (var item in _required)
                   {
                       session.Data.Inventory.Remove(item._id, item._value); 
                   }
               }
               _onSucsess?.Invoke();       
           }
           else
           { 
               _onFail?.Invoke();   
           }
       }
    }
}