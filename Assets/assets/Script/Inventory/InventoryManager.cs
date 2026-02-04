using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using assets.Script.Inventory.DragManager;

public class InventoryManager : MonoBehaviour
{
    
    public string inventoryName;
    
    public DragManager DragManager;
    
    public List<InventorySlot> _slots  = new List<InventorySlot>();
    

    private void Start()
    {
        for (var i = 0; i < _slots.Count; i++)
        {
            _slots[i].dragManager = DragManager;
            Debug.Log($"Slot Id: {_slots[i].slotId} itemId {_slots[i].itemID} amount {_slots[i].amount} ItemName {_slots[i].itemName}");
        }
        AddItemForSlot(1, 10, "карась");
        AddItemForSlot(2, 11, "пиво");
    }
    
    
    public void AddItemForSlot(int advancedItemId, int advancedAmount, string advancedItemName)
    {
        
        List<InventorySlot> emptySlots  = new List<InventorySlot>();
        
        for (var i = 0; i < _slots.Count; i++)
        {
            if (_slots[i].itemID == advancedItemId && _slots[i].amount < _slots[i].itemMaxStack)
            {
                emptySlots.Add(_slots[i]);
                Debug.Log($"Слот: {_slots[i].slotId} занят таким же предметом, может подойти");
            }
            else if (_slots[i].itemID == 0)
            {
                emptySlots.Add(_slots[i]);     
                Debug.Log($"Слот: {_slots[i].slotId} пуст он может подойти");
            }
            else
            {
                Debug.Log($"Слот: {_slots[i].slotId} занят, не подходит");
            }
        }
        
        emptySlots.Sort((a, b) => {
            if (a.itemID == advancedItemId && b.itemID == 0) return -1;
            if (a.itemID == 0 && b.itemID == advancedItemId) return 1;
            
            return a.amount.CompareTo(b.amount);
        });
        
        for (var y = 0; y < emptySlots.Count; y++)
        {
            
            var comparable =  emptySlots[y].itemMaxStack - emptySlots[y].amount;
            
            if (comparable >= advancedAmount)
            { 
                emptySlots[y].AddItem(advancedItemId, advancedAmount, advancedItemName); 
                Debug.Log($"Slot Id: {emptySlots[y].slotId} itemId {emptySlots[y].itemID} amount {emptySlots[y].amount} ItemName {emptySlots[y].itemName}");
                advancedAmount = 0;
                break;
            }
            else
            {
                int toAdd = Mathf.Min(comparable, advancedAmount);
                emptySlots[y].AddItem(advancedItemId, toAdd, advancedItemName); 
                advancedAmount -= toAdd;
                if (advancedAmount < 0)  advancedAmount = 0;
            }
            
            Debug.Log($"Slot Id: {emptySlots[y].slotId} itemId {emptySlots[y].itemID} amount {emptySlots[y].amount} ItemName {emptySlots[y].itemName}");
        }
        
        if (advancedAmount == 0)
        {
            return;
        }
        else
        {
            Debug.Log("Места не достаточно Вьюжник...");
        }
    }

    public void AddItemForSlotById(int slotId, int advancedItemId, int advancedAmount, string advancedItemName)
    {
        _slots[slotId].AddItem(advancedItemId, advancedAmount, advancedItemName);
        Debug.Log($"Slot Id: {_slots[slotId]} itemIdAdded {_slots[slotId].itemID} amountAdded {_slots[slotId].amount} ItemNameAdded {_slots[slotId].itemName}");
    }
    
    
    public void RemoveItemForSlot(int itemRemoveId,  int removeAmount)
    {
        for (int i = 0; i < _slots.Count; i++)
        {
            if (_slots[i].itemID == itemRemoveId)
            {
                if (_slots[i].amount >= removeAmount)
                { 
                    _slots[i].RemoveItem(removeAmount); 
                    Debug.Log($"SlotId: {_slots[i].slotId}  itemIdRemoved {_slots[i].itemID} New Amount {_slots[i].amount}");
                }
                else
                {
                    removeAmount -= _slots[i].amount ;
                    _slots[i].RemoveItem(_slots[i].amount);
                }
            }
        }

        if (removeAmount == 0)
        {
            return;
        }
        else
        {
            Debug.Log("Хабара не достаточно Вьюжник...");
        }
    }
    
    public void RemoveItemForSlotById(int removeItemToSlotId, int removeAmount)
    {
        _slots[removeItemToSlotId].RemoveItem(removeAmount);
        Debug.Log($"SlotId: {_slots[removeItemToSlotId].slotId}  itemIdRemoved {_slots[removeItemToSlotId].itemID} New Amount {_slots[removeItemToSlotId].amount}");
    }


    public IEnumerator TimeStop()
    {
        yield return new WaitForSeconds(3f);
        RemoveItemForSlot(2, 10);
    }

}
