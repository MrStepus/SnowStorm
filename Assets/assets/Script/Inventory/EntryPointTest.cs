using System.Collections.Generic;
using assets.Script.Inventory;
using UnityEngine;

public class EntryPointTest : MonoBehaviour
{
    public InventoryGridView _view;
    private InventoryService _inventoryService;

    private void Start()
    {
        _inventoryService = new InventoryService();
        var ownerId = "MrStepus";
        var inventoryData =  CreateTestInventory(ownerId);
        var inventory = _inventoryService.RegisterInventory(inventoryData);
        
        _view.Setup(inventory);

        var addedResult = _inventoryService.AddItemsToInventory(ownerId, "????", 101);
        Debug.Log($"? ??????? ? {addedResult}");
        addedResult = _inventoryService.AddItemsToInventory(ownerId, "???????? ???????", 1); 
        Debug.Log($"? ??????? ? {addedResult}");
        
        _view.Print();
        
        addedResult = _inventoryService.AddItemsToInventory(ownerId, "????", 101);
        Debug.Log($"? ??????? ? {addedResult}");
        addedResult = _inventoryService.AddItemsToInventory(ownerId, "???????? ????", 1); 
        Debug.Log($"? ??????? ? {addedResult}");
        
        _view.Print();
        
        addedResult = _inventoryService.AddItemsToInventory(ownerId, "????????", 10); 
        Debug.Log($"? ??????? ? {addedResult}");
        
        _view.Print();
    }

    private InventoryGridData CreateTestInventory(string ownerId)
    {
        var size = new Vector2Int(3, 4);
        var createdInventorySlots = new List<InventorySlotData>();
        var length =  size.x *  size.y;
        for (var i = 0; i < length; i++)
        {
            createdInventorySlots.Add(new InventorySlotData());
        }

        var createdInventoruData = new InventoryGridData
        {
            ownerId = ownerId,
            size = size,
            Slots = createdInventorySlots
        };
        
        return createdInventoruData;
    }
}
