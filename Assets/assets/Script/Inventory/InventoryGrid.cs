using System;
using System.Collections.Generic;
using UnityEngine;
using assets.Script.Inventory;

public class InventoryGrid : MonoBehaviour, IReadOnlyInventoryGrid
{
    public event Action<string, int> ItemsAdded;
    public event Action<string, int> ItemsRemoved;
    public event Action<Vector2Int> sizeChanged;


    public string ownerId => _data.ownerId;
    public Vector2Int Size
    {
        get => _data.size;

        set
        {
            if (_data.size != value)
            {
                _data.size = value;
                sizeChanged?.Invoke(value);
            }
        }
    }

    private readonly InventoryGridData _data;
    private readonly Dictionary<Vector2Int, InventorySlot> _slotsMap = new();

    public InventoryGrid(InventoryGridData data)
    {
        _data = data; 

        var size = _data.size;
        for (var x = 0; x < size.x; x++)
        {
            for (var y = 0; y < size.y; y++)
            {
                var index = x * size.y + y;
                var slotData = _data.Slots[index];
                var slot = new InventorySlot(slotData);
                var position = new Vector2Int(x, y);

                _slotsMap[position] = slot;
            }
        }
    }
    
    public AddItemsToInvenroryGridResult AddItems(string itemId, int amount = 1)
    {
        var remainingAmount = amount;
        var itemsAddedToSlotsWithSameItemsAmount = AddToSlotsWithSameItems(itemId, remainingAmount, out remainingAmount);

        if (remainingAmount <= 0)
        {
            return new AddItemsToInvenroryGridResult(ownerId, amount, itemsAddedToSlotsWithSameItemsAmount);
        }
        
        var itemsAddedToAvaibleSlotAmount = AddToFristAveilableSlots(itemId, remainingAmount, out remainingAmount);
        var totalAddedItemsAmount = itemsAddedToSlotsWithSameItemsAmount + itemsAddedToAvaibleSlotAmount;
        
        return new AddItemsToInvenroryGridResult(ownerId, amount, totalAddedItemsAmount);
    }

    public AddItemsToInvenroryGridResult AddItems(Vector2Int slotCords, string itemId, int amount = 1)
    {

        var slot = _slotsMap[slotCords];
        var newValue = slot.amount + amount;
        var itemsAddedAmount = 0;

        if (slot.isEmpty)
        {
            slot.itemId = itemId;
        }
        
        var itemSlotCapacity = GetItemSlotCapasity(itemId);

        if (newValue > itemSlotCapacity)
        {
            var remainingItems = newValue - itemSlotCapacity;
            var itemsToAddAmount = itemSlotCapacity - slot.amount;
            itemsAddedAmount += itemsToAddAmount;
            slot.amount = itemSlotCapacity;

            var result = AddItems(itemId, remainingItems);
            itemsAddedAmount += result.ItemsAddedAmount;
        }
        else
        {
            itemsAddedAmount = amount;
            slot.amount = newValue;
        }
        return new AddItemsToInvenroryGridResult(ownerId, amount, itemsAddedAmount);

    }

    public RemoveItemsFromInventoryGridResult RemoveItems(string itemId, int amount = 1)
    {
        if (!Has(itemId, amount))
        {
            return new RemoveItemsFromInventoryGridResult(ownerId, amount, false);
        }
        
        var amountToRemove = amount;

        for (var x = 0; x < Size.x; x++)
        {
            for (var y = 0; y < Size.y; y++)
            {
                var slotCords = new Vector2Int(x, y);
                var slot = _slotsMap[slotCords];

                if (slot.itemId != itemId)
                {
                    continue;
                }

                if (amountToRemove > slot.amount)
                {
                    amountToRemove -= slot.amount;
                    
                    RemoveItems(slotCords, itemId, slot.amount);
                    
                }
                else
                {
                    RemoveItems(slotCords, itemId, amountToRemove);
                    
                    return new RemoveItemsFromInventoryGridResult(ownerId, amount, true);
                }
            }
        }
        throw new Exception("Something went wrong, couldn't remove some items");
    }

    public RemoveItemsFromInventoryGridResult RemoveItems(Vector2Int slotCords, string itemId, int amount = 1)
    {

        var slot = _slotsMap[slotCords];

        if (slot.isEmpty || slot.itemId !=  itemId || slot.amount < amount)
        {
            return new RemoveItemsFromInventoryGridResult(ownerId, amount, false);
        }

        slot.amount -= amount;

        if (slot.amount == 0)
        {
            slot.itemId = null;
        }

        return new RemoveItemsFromInventoryGridResult(ownerId, amount, true);

    }


    public int GetAmount(string itemId)
    {
        var amount = 0;
        var slots = _data.Slots;

        foreach (var slot in slots)
        {
            if (slot.itemId == itemId)
            {
                amount += slot.amount;
            }
        }
        
        return amount;
    }

    public bool Has(string itemId, int amount)
    {
        var amountExist = GetAmount(itemId);
        return amountExist >= amount;
    }

    public void SwithSlots(Vector2Int slotCordsA, Vector2Int slotCordsB)
    {
        var slotA = _slotsMap[slotCordsA];
        var slotB = _slotsMap[slotCordsB];
        var tempSlotItemId = slotA.itemId;
        var tempSlotItemAount = slotA.amount;
        slotA.itemId = slotB.itemId;
        slotA.amount = slotB.amount;
        slotB.itemId = tempSlotItemId;
        slotB.amount = tempSlotItemAount;
    }

    public void Setsize(Vector2Int newSize)
    {
        throw new NotImplementedException();
    }

    public IReadOnlyInventorySlot[,] GetSlots()
    {
        var array = new IReadOnlyInventorySlot[Size.x, Size.y];

        for (var x = 0; x < Size.x; x++)
        {
            for (var y = 0; y < Size.y; y++)
            {
                var position = new Vector2Int(x, y);
                array[x, y] = _slotsMap[position];
            }
        }

        return array;
    }

    private int AddToSlotsWithSameItems(string itemId, int amount, out int remainingAmount)
    {

        var itemsAddedAmount = 0;
        remainingAmount = amount;
        
        for (var x = 0; x < Size.x; x++)
        {
            for (var y = 0;  y < Size.y; y++)
            {
                var cords = new Vector2Int(x, y);
                var slot = _slotsMap[cords];

                if (slot.isEmpty)
                {
                    continue;    
                }

                var slotItemCopacity = GetItemSlotCapasity(slot.itemId);
                if (slot.amount >= slotItemCopacity) { continue; }

                if (slot.itemId != itemId) { continue; }

                var newValue = slot.amount + remainingAmount;

                if (newValue > slotItemCopacity)
                {
                    remainingAmount = newValue - slotItemCopacity;
                    var itemsToAddAmount = slotItemCopacity - slot.amount;
                    itemsAddedAmount += itemsToAddAmount;
                    slot.amount = slotItemCopacity;

                    if (remainingAmount == 0)
                    {
                        return itemsAddedAmount;
                    }
                }
                else
                {
                    itemsAddedAmount += remainingAmount;
                    slot.amount = newValue;
                    remainingAmount = 0;

                    return itemsAddedAmount;
                }

            }
        }

        return itemsAddedAmount;
    }


    private int AddToFristAveilableSlots(string itemId, int amount, out int remainingAmount)
    {
        var itemsAddedAmount = 0;
        remainingAmount = amount;

        for (var x = 0; x < Size.x; x++)
        {
            for (var y = 0; y < Size.y; y++)
            {
                var cords = new Vector2Int(x, y);
                var slot = _slotsMap[cords];

                if (!slot.isEmpty)
                {
                    continue;
                }
                
                slot.itemId = itemId;
                var newValue = remainingAmount;
                var slotItemCopacity = GetItemSlotCapasity(slot.itemId);

                if (newValue > slotItemCopacity)
                {
                    remainingAmount = newValue - slotItemCopacity;
                    var itemsToAddAmount = slotItemCopacity;
                    itemsAddedAmount += itemsToAddAmount;
                    slot.amount = slotItemCopacity;
                }
                else
                {
                    itemsAddedAmount += remainingAmount;
                    slot.amount = newValue;
                    remainingAmount = 0;
                    
                    return itemsAddedAmount;
                }
            }
        }
        return itemsAddedAmount;
    }

    private int GetItemSlotCapasity(string itemId)
    {
        return 99;
    }

}
