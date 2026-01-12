using System;
using UnityEngine;

public interface IReadOnlyInventorySlot
{

    event Action<string> itemIdChanged;
    event Action<int> itemAmountChanged;

    string itemId {  get; }
    int amount { get; }
    bool isEmpty { get; }

}
