using UnityEngine;

namespace assets.Script.Inventory.DragManager
{
    public class DragManager : MonoBehaviour
    {
        public InventoryManager inv;
        public DragCursor dragCursor;

        public bool emptyDragCursor  = true;
        
        public int dragAmount;

        public string aName;
        public string bName;
        
        public int aItemID;
        public int bItemID;
        
        public int itemMaxStack;
        
        public int aSlotID;
        public int bSlotID;

        public void ItemDragSlot(int amount, string itemName,  int itemID, int maxItemStack, int slotID)
        {
            
            emptyDragCursor = false;
            
            dragAmount = amount;
            aName = itemName;
            aItemID = itemID;
            itemMaxStack = maxItemStack;
            aSlotID = slotID;
            
            inv._slots[slotID].itemID = 0;
            inv._slots[slotID].itemName = " ";
            inv._slots[slotID].titleText.text = "";
            inv._slots[slotID].amount = 0;
            inv._slots[slotID].amountTitle.text = inv._slots[slotID].amount.ToString(); 
            
            dragCursor.setDragCursor(dragAmount);
            
        }

        public void ItemDropSlot(int amount, string itemName, int itemID, int maxItemStack, int slotID)
        {

            if (inv._slots[slotID].itemID == 0)
            {
                inv._slots[slotID].AddItem(itemID, dragAmount, aName);
            }
            
            emptyDragCursor = true;
            dragCursor.clearDragCursor();
            
        }

    }
}