using System;
using UnityEngine;

public interface IReadOnlyInventoryGrid : IReadOnlyInventory
{

    event Action<Vector2Int> sizeChanged;

    Vector2Int size { get; }

    IReadOnlyInventorySlot[,] GetSlots();

}
