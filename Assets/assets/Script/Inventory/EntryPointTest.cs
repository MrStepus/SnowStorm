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

        var addedResult = _inventoryService.AddItemsToInventory(ownerId, "чай", 101);
        Debug.Log($"я добавил в инвеентарь {addedResult.InventoryOwnerId}, предмет чай в количестве {addedResult.ItemsToAddAmount}. удалось добавить {addedResult.ItemsAddedAmount}");
        addedResult = _inventoryService.AddItemsToInventory(ownerId, "сломаная катана?", 1); 
        Debug.Log($"я добавил в инвеентарь {addedResult.InventoryOwnerId}, предмет сломаная катана? в количестве {addedResult.ItemsToAddAmount}. удалось добавить {addedResult.ItemsAddedAmount}");
        
        _view.Print();
        
        addedResult = _inventoryService.AddItemsToInventory(ownerId, "чай", 101);
        Debug.Log($"я добавил в инвеентарь {addedResult.InventoryOwnerId}, предмет чай в количестве {addedResult.ItemsToAddAmount}. удалось добавить {addedResult.ItemsAddedAmount}");
        addedResult = _inventoryService.AddItemsToInventory(ownerId, "дигл?", 1); 
        Debug.Log($"я добавил в инвеентарь {addedResult.InventoryOwnerId}, предмет дигл? в количестве {addedResult.ItemsToAddAmount}. удалось добавить {addedResult.ItemsAddedAmount}");
        
        _view.Print();
        
        addedResult = _inventoryService.AddItemsToInventory(ownerId, "кислый мармелад", 10); 
        Debug.Log($"я добавил в инвеентарь {addedResult.InventoryOwnerId}, предмет кислый мармелад в количестве {addedResult.ItemsToAddAmount}. удалось добавить {addedResult.ItemsAddedAmount}");
        
        _view.Print();
        
        var removeResult = _inventoryService.RemoveItems(ownerId, "кислый мармелад", 2); 
        Debug.Log($"я съел из инвентаря {removeResult.InventoryOwnerId}, предмет кислый мармелад в количестве {removeResult.ItemsToRemoveAmount}. удалось ли? {removeResult.Success}");
        
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

        var createdInventoryData = new InventoryGridData
        {
            ownerId = ownerId,
            size = size,
            Slots = createdInventorySlots
        };
        
        return createdInventoryData;
    }
}
