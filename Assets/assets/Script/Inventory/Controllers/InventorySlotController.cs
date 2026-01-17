using assets.Script.Inventory.Views;

namespace assets.Script.Inventory.Controllers
{
    public class InventorySlotController
    {
        private readonly InventorySlotView _view;
        
        public InventorySlotController(IReadOnlyInventorySlot slot,  InventorySlotView view)
        {
            _view  = view;

            slot.itemIdChanged += OnSlotItemIdChanged;
            slot.itemAmountChanged += OnSlotItemAmountChanged;

            view.Titel = slot.itemId;
            view.TextAmount = slot.amount;
        }

        private void OnSlotItemAmountChanged(int newAmount)
        {
            _view.TextAmount = newAmount;
        }

        private void OnSlotItemIdChanged(string newItemId)
        {
            _view.Titel = newItemId;
        }
    }
}