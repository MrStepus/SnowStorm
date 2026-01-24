using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    
    public string inventoryName;
    
    public List<InventorySlot> _slots  = new List<InventorySlot>();

    private void Start()
    {
        for (var i = 0; i < _slots.Count; i++)
        {
            Debug.Log($"Slot Id: {i} itemId {_slots[i].itemID} amount {_slots[i].amount} ItemName {_slots[i].itemName}");
        }
        
        AddItemForSlot(0, 1, "f");
        AddItemForSlotById(0, 0, 1, "Пиво");
        StartCoroutine(TimeStop());
    }
    
    
    public void AddItemForSlot(int advancedItemId, int advancedAmount, string advancedItemName)
    {
        for (var i = 0; i < _slots.Count; i++)
        {

            
            
        }
    }

    public void AddItemForSlotById(int slotId, int advancedItemId, int advancedAmount, string advancedItemName)
    {
        _slots[slotId].AddItem(advancedItemId, advancedAmount, advancedItemName);
        Debug.Log($"Slot Id: {_slots[slotId]} itemIdAdded {_slots[slotId].itemID} amountAdded {_slots[slotId].amount} ItemNameAdded {_slots[slotId].itemName}");
    }
    
    
    public void RemoveItemForSlot(int itemRemoveId,  int removeAmount)
    {
        for (var i = 0; i < _slots.Count; i++)
        {
            if (_slots[i].itemID == itemRemoveId)
            {
                _slots[i].RemoveItem(removeAmount);
                Debug.Log($"SlotId: {_slots[i]}  itemIdRemoved {_slots[i].itemID} New Amount {_slots[i].amount}");
            }
        }
    }
    
    public void RemoveItemForSlotById(int removeItemToSlotId, int removeAmount)
    {
        _slots[removeItemToSlotId].RemoveItem(removeAmount);
        Debug.Log($"SlotId: {_slots[removeItemToSlotId]}  itemIdRemoved {_slots[removeItemToSlotId].itemID} New Amount {_slots[removeItemToSlotId].amount}");
    }


    public IEnumerator TimeStop()
    {
        yield return new WaitForSeconds(3f);
        RemoveItemForSlot(2, 1);
    }

}
