using System;
using UnityEngine;

public interface IReadOnlyInventorySlot
{

    event Action<int> itemIdChanged;
    event Action<int> itemAmountChanged;

    int itemId {  get; }
    int amount { get; }
    bool isEmpty { get; }

}
