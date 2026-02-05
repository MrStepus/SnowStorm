using System;
using UnityEngine;

public class InventorySlot : IReadOnlyInventorySlot
{
    
    public ItemDatabase.ItemConfig Config { get; private set; }

    private readonly InventorySlotData _data;

    public event Action<int> itemIdChanged;
    public event Action<int> itemAmountChanged;

    public int itemId 
    { 

        get => _data.itemId;

        set
        {
            if (_data.itemId != value)
            {
                _data.itemId = value;
                Config = ItemDatabase.GetConfig(value);
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
    public bool isEmpty => amount == 0 && itemId <= 0;

    public InventorySlot(InventorySlotData data)
    {
        _data = data;
        if (_data.itemId > 0) Config = ItemDatabase.GetConfig(_data.itemId);
    }

}
