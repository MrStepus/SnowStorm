using System;
using UnityEngine;

public interface IReadOnlyInventory 
{

    event Action<string, int> ItemsAdded;
    event Action<string, int> ItemsRemoved;

    string ownerId { get;}

    int GetAmount(string itemId);
    bool has(string itemId, int amount);

}
