using System;
using UnityEngine;

public class InventoryGrid : MonoBehaviour, IReadOnlyInventoryGrid
{

    public event Action<string, int> ItemAdded;
    public event Action<string, int> ItemRemoved;
    public event Action<Vector2Int> sizeChanged;

    public string ownerId => _data.ownerId;
    public Vector2Int size {  get; }

    private readonly InventoryGridData _data;

    public InventoryGrid(InventoryGridData data)
    {
        _data = data; 
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
        throw new NotImplementedException();
    }   
}
