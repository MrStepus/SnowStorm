using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using assets.Script.Inventory.DragManager;

public class InventoryManager : MonoBehaviour
{
    // ===== ID владельца инвентаря =====
    [Header("Владелец инвентаря")]
    [Tooltip("Уникальный ID владельца (Player, Chest_1, Merchant и т.д.)")]
    public string ownerId = "Player";
    
    [Header("Настройки инвентаря")]
    public string inventoryName;
    
    public DragManager dragManager;
    
    public List<InventorySlot> _slots = new List<InventorySlot>();
    
    private void Start()
    {
       ItemDatabase.Load();
        // Инициализация слотов
        for (var i = 0; i < _slots.Count; i++)
        {
            _slots[i].dragManager = dragManager;
            _slots[i].slotId = i;
            
            // ===== НОВОЕ: привязываем слот к этому инвентарю =====
            _slots[i].parentInventory = this;
            
        }
        
        AddItemForSlot(26050102, 2);
        AddItemForSlot(26050102, 2);
        AddItemForSlotById(5, 26050202, 1) ;
    }
    
    public void AddItemForSlot(int advancedItemId, int advancedAmount)
    {
        List<InventorySlot> emptySlots = new List<InventorySlot>();
        
        for (var i = 0; i < _slots.Count; i++)
        {
            var conf = ItemDatabase.GetConfig(advancedItemId);
            if (_slots[i].itemID == advancedItemId && _slots[i].amount < conf.maxStack && _slots[i].slotType == conf.itemType || _slots[i].slotType == "any")
            {
                emptySlots.Add(_slots[i]);
                Debug.Log($"[{ownerId}] Слот: {_slots[i].slotId} занят таким же предметом, может подойти");
            }
            else if (_slots[i].itemID == 11111111 && _slots[i].slotType == conf.itemType || _slots[i].slotType == "any")
            {
                emptySlots.Add(_slots[i]);     
                Debug.Log($"[{ownerId}] Слот: {_slots[i].slotId} пуст он может подойти");
            }
            else
            {
                Debug.Log($"[{ownerId}] Слот: {_slots[i].slotId} занят, не подходит");
            }
        }
        
        emptySlots.Sort((a, b) => {
            if (a.itemID == advancedItemId && b.itemID == 11111111) return -1;
            if (a.itemID == 11111111 && b.itemID == advancedItemId) return 1;
            return a.amount.CompareTo(b.amount);
        });
        
        for (var y = 0; y < emptySlots.Count; y++)
        {
            var conf = ItemDatabase.GetConfig(advancedItemId);
            var comparable = conf.maxStack - emptySlots[y].amount;
            
            if (comparable >= advancedAmount)
            { 
                emptySlots[y].AddItem(advancedItemId, advancedAmount); 
                Debug.Log($"[{ownerId}] Slot Id: {emptySlots[y].slotId} itemId {emptySlots[y].itemID} amount {emptySlots[y].amount} ItemName {conf.displayName}");
                advancedAmount = 0;
                break;
            }
            else
            {
                int toAdd = Mathf.Min(comparable, advancedAmount);
                emptySlots[y].AddItem(advancedItemId, toAdd); 
                advancedAmount -= toAdd;
                if (advancedAmount < 0) advancedAmount = 0;
            }
            
            Debug.Log($"[{ownerId}] Slot Id: {emptySlots[y].slotId} itemId {emptySlots[y].itemID} amount {emptySlots[y].amount} ItemName {conf.displayName}");
        }
        
        if (advancedAmount == 0)
        {
            return;
        }
        else
        {
            Debug.Log($"[{ownerId}] Места не достаточно Вьюжник...");
        }
    }

    public void AddItemForSlotById(int slotId, int advancedItemId, int advancedAmount)
    {
        _slots[slotId].AddItem(advancedItemId, advancedAmount);
        var conf = ItemDatabase.GetConfig(_slots[slotId].itemID);
        Debug.Log($"[{ownerId}] Slot Id: {_slots[slotId]} itemIdAdded {_slots[slotId].itemID} amountAdded {_slots[slotId].amount} ItemNameAdded {conf.displayName}");
    }
    
    public void RemoveItemForSlot(int itemRemoveId, int removeAmount)
    {
        for (int i = 0; i < _slots.Count; i++)
        {
            if (_slots[i].itemID == itemRemoveId)
            {
                if (_slots[i].amount >= removeAmount)
                { 
                    _slots[i].RemoveItem(removeAmount); 
                    Debug.Log($"[{ownerId}] SlotId: {_slots[i].slotId} itemIdRemoved {_slots[i].itemID} New Amount {_slots[i].amount}");
                }
                else
                {
                    removeAmount -= _slots[i].amount;
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
            Debug.Log($"[{ownerId}] Хабара не достаточно Вьюжник...");
        }
    }
    
    public void RemoveItemForSlotById(int removeItemToSlotId, int removeAmount)
    {
        _slots[removeItemToSlotId].RemoveItem(removeAmount);
        Debug.Log($"[{ownerId}] SlotId: {_slots[removeItemToSlotId].slotId} itemIdRemoved {_slots[removeItemToSlotId].itemID} New Amount {_slots[removeItemToSlotId].amount}");
    }

    public IEnumerator TimeStop()
    {
        yield return new WaitForSeconds(3f);
        RemoveItemForSlot(2, 10);
    }
}