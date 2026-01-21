namespace assets.Script.Inventory
{
    using System.Collections.Generic;
    using UnityEngine;
    
    public class DragBuffer
    {
        public string ItemId;
        public int Amount;
        public bool IsEmpty => string.IsNullOrEmpty(ItemId);

        public void Clear()
        {
            ItemId = null;
            Amount = 0;
        }
    }
    
    public class InventoryService 
    {
        
        public DragBuffer CurrentBuffer { get; } = new DragBuffer();
        private readonly Dictionary< string, InventoryGrid>  _inventoriesMap = new();

        public InventoryGrid RegisterInventory(InventoryGridData inventoryData)
        {
            var  inventory = new InventoryGrid(inventoryData);
            _inventoriesMap[inventoryData.ownerId] = inventory;
            
            return inventory;
        }
        
        public AddItemsToInvenroryGridResult AddItemsToInventory(string ownerId, string itemId, int amount = 1)
        {
            var inventory = _inventoriesMap[ownerId];
            return inventory.AddItems(itemId, amount);
        }
        
        public AddItemsToInvenroryGridResult AddItemsToInventory(
            string ownerId,
            Vector2Int slotCords,
            string itemId,
            int amount = 1)
        {
            var inventory = _inventoriesMap[ownerId];
            return inventory.AddItems(slotCords, itemId, amount);
        }

        public RemoveItemsFromInventoryGridResult RemoveItems(string ownerId, string itemId, int amount = 1)
        {
            var inventory = _inventoriesMap[ownerId];
            return inventory.RemoveItems(itemId, amount);
        }
        
        public RemoveItemsFromInventoryGridResult RemoveItemsFromSlot(
            string ownerId,
            Vector2Int slotCords,
            string itemId,
            int amount = 1)
        {
            var inventory = _inventoriesMap[ownerId];
            return inventory.RemoveItems(slotCords, itemId, amount);
        }

        public bool Has(string ownerId, string itemId, int amount = 1)
        {
            var inventory = _inventoriesMap[ownerId];
            return inventory.Has(itemId, amount);
        }

        public IReadOnlyInventoryGrid GetInventory(string ownerId)
        {
            return _inventoriesMap[ownerId];
        }
        
    }
}