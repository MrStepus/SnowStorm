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

            var config = ItemDatabase.GetConfig(slot.itemId);
            
            if (config != null)
            {
                _view.Titel = config.displayName; 
            }
            else
            {
                _view.Titel = ""; 
            }
            view.TextAmount = slot.amount;
        }

        private void OnSlotItemAmountChanged(int newAmount)
        {
            _view.TextAmount = newAmount;
        }

        private void OnSlotItemIdChanged(int newItemId)
        {
            var config = ItemDatabase.GetConfig(newItemId);
            if (config != null)
            {
                _view.Titel = config.displayName; // Теперь это строка "Tea", а не "1"
            }
            else
            {
                _view.Titel = ""; 
            }
        }
    }
}