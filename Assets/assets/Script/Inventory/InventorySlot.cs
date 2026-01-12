using System;
using UnityEngine;

public class InventorySlot : IReadOnlyInventorySlot
{

    private readonly InventorySlotData _data;

    public event Action<string> itemIdChanged;
    public event Action<int> itemAmountChanged;

    public string itemId 
    { 

        get => _data.itemId;

        set
        {
            if (_data.itemId != value)
            {
                _data.itemId = value;
                itemIdChanged?.Invoke(value);
            }
        }

    }
    public int amount
    {

        get => _data.amount;

        set
        {
            if (_data.amount != value)
            {
                _data.amount = value;
                itemAmountChanged?.Invoke(value);
            }
        }

    }
    public bool isEmpty => amount == 0 && string.IsNullOrEmpty(itemId);

    public InventorySlot(InventorySlotData data)
    {
        _data = data;
    }

}
