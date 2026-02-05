namespace assets.Script.Inventory
{
    public readonly struct AddItemsToInvenroryGridResult
    {

        public readonly string InventoryOwnerId;
        public readonly int ItemsToAddAmount;
        public readonly int ItemsAddedAmount;
        
        public int ItemsHotAddedAmount => ItemsToAddAmount -  ItemsAddedAmount;

        public AddItemsToInvenroryGridResult(string inventoryOwnerId, int itemsToAddAmount, int itemsAddedAmount)
        {
            InventoryOwnerId = inventoryOwnerId;
            ItemsToAddAmount = itemsToAddAmount;
            ItemsAddedAmount = itemsAddedAmount;
        }
    }
}