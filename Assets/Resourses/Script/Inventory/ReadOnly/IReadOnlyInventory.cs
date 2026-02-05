using System;
using UnityEngine;

public interface IReadOnlyInventory 
{

    event Action<int, int> ItemsAdded;
    event Action<int, int> ItemsRemoved;

    string ownerId { get;}

    int GetAmount(int itemId);
    bool Has(int itemId, int amount);

}
