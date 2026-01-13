using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryGridData : MonoBehaviour
{

    public string ownerId;
    public List<InventorySlotData> Slots;
    public Vector2Int size;

}
