using System;
using System.Collections.Generic;
using UnityEngine;

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


    public void AddItems(string itemId, int amount = 1)
    {

    }

    public void AddItems(Vector2Int slotCords, string itemId, int amount = 1)
    {

    }

    public void RemoveItems(string itemId, int amount = 1)
    {

    }

    public void RempveItems(Vector2Int slotCords, string itemId, int amount = 1)
    {

    }


    public int GetAmount(string itemId)
    {
        throw new NotImplementedException();
    }

    public bool Has(string itemId, int amount)
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
}
