using System.Collections.Generic;
using assets.Script.Inventory;
using assets.Script.Inventory.Controllers;
using assets.Script.Inventory.Views;
using UnityEngine;

public class EntryPointTest : MonoBehaviour
{
    [SerializeField] private ScreenView _screenView;
    private InventoryService _inventoryService;
    private ScreenController _screenController;
    private string _cachedOwnerId;
    private readonly string[] _itemIds = { "Tea", "Katan", "Amogus", "Masck" };

    private const string Owner_1 = "MrStepus";
    private const string Owner_2 = "X";
    

    private void Start()
    {
        _inventoryService = new InventoryService();
        
        var inventorydataMrStepus = CreateTestInventory(Owner_1);
        _inventoryService.RegisterInventory(inventorydataMrStepus);
        
        var inventorydataX = CreateTestInventory(Owner_1);
        _inventoryService.RegisterInventory(inventorydataX);
        
        _screenController = new ScreenController(_inventoryService, _screenView);
        _screenController.OpenInvwntory(Owner_1);
        _cachedOwnerId = Owner_1;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            var rIndex = Random.Range(0, _itemIds.Length);
            var rItemId = _itemIds[rIndex];
            var rAmount = Random.Range(1, 50);
            var result = _inventoryService.AddItemsToInventory(_cachedOwnerId, rItemId, rAmount);
            
            Debug.Log($"Item added: ${rItemId}. Amount added: {result.ItemsAddedAmount}");
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            var rIndex = Random.Range(0, _itemIds.Length);
            var rItemId = _itemIds[rIndex];
            var rAmount = Random.Range(1, 50);
            var result = _inventoryService.RemoveItems(_cachedOwnerId, rItemId, rAmount);
            
            Debug.Log($"Item added: ${rItemId}. Amount added: {result.ItemsToRemoveAmount}, Success: {result.Success}");
        }
        
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
