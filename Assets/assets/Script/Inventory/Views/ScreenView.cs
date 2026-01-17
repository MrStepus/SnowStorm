using UnityEngine;

namespace assets.Script.Inventory.Views
{
    public class ScreenView : MonoBehaviour
    {
         [SerializeField] private InventoryView _inventoryView;
         
         public InventoryView InventoryView => _inventoryView;
    }
}