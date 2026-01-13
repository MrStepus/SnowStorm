using System.Collections.Generic; 
using UnityEngine;

public class EntryPointTest : MonoBehaviour
{
    public InventoryGridView _view;

    private void Start()
    {
        var inventoryData = new InventoryGridData
        {
            size = new Vector2Int(3, 4),
            ownerId = "MrStepus",
            Slots = new List<InventorySlotData>()
        };

        var size = inventoryData.size;
        for (var x = 0; x < size.x; x++)
        {
            for (var y = 0; y < size.y; y++)
            {
                var index = x * size.x + y;
                inventoryData.Slots.Add(new InventorySlotData());
            }
        }

        var inventory = new InventoryGrid(inventoryData);

        var slotData = inventoryData.Slots[0];
        slotData.itemId = "Пиво";
        slotData.amount = 15;


        _view.Setup(inventory);

    }
}
