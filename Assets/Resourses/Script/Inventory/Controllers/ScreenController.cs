using assets.Script.Inventory.Views;
using assets.Script.Inventory;

namespace assets.Script.Inventory.Controllers
{
    public class ScreenController
    {
        
        private readonly InventoryService _inventoryService;
        private readonly ScreenView _view;


        private InventoryGridController _currentInventoryController;
        
        
        public ScreenController(InventoryService inventoryService, ScreenView view)
        {
            _inventoryService = inventoryService;
            _view = view;
        }

        public void OpenInvwntory(string ownerId )
        {
            var inventory = _inventoryService.GetInventory(ownerId);
            var inventoryView = _view.InventoryView;
            
            _currentInventoryController = new InventoryGridController(inventory, inventoryView);
        }
    }
}