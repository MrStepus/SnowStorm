using UnityEngine;

namespace assets.Script.Inventory.DragManager
{
    public class DragManager : MonoBehaviour
    {
        public InventoryManager inv;
        public DragCursor dragCursor;

        public bool emptyDragCursor  = true;
        
        public int dragAmount;
        public int bAmount;

        public string aName;
        public string bName;
        
        public int aItemID;
        public int bItemID;
        
        public int aItemMaxStack;
        public int bItemMaxStack;
        
        public int aSlotID;
        public int bSlotID;

        public void ItemDragSlot(int amount, string itemName,  int itemID, int maxItemStack, int slotID)
        {
            
            emptyDragCursor = false;
            
            dragAmount = amount;
            aName = itemName;
            aItemID = itemID;
            aItemMaxStack = maxItemStack;
            aSlotID = slotID;
            
            inv._slots[slotID].itemID = 0;
            inv._slots[slotID].itemName = " ";
            inv._slots[slotID].titleText.text = "";
            inv._slots[slotID].amount = 0;
            inv._slots[slotID].amountTitle.text = inv._slots[slotID].amount.ToString();

            if (dragAmount != 0)
            {
                dragCursor.setDragCursor(dragAmount);                
            }
            
        }

        public void ItemDropSlot(int amount, string itemName, int itemID, int maxItemStack, int slotID)
        {

            if (inv._slots[slotID].itemID == 0)
            {
                inv._slots[slotID].AddItem(aItemID, dragAmount, aName);
                emptyDragCursor = true;            
                dragCursor.clearDragCursor();
            }

            if (inv._slots[slotID].itemID != 0 && inv._slots[slotID].itemID != aItemID)
            { 
                ItemSwap(amount, itemName, itemID, maxItemStack, slotID);  
            }
            
            
            
        }

        public void ItemSwap(int amount, string itemName, int itemID, int maxItemStack, int slotID)
        {
            
            bName = itemName;
            bItemID = itemID;
            bItemMaxStack = maxItemStack;
            bAmount = amount;
            
            inv._slots[slotID].itemName = aName;
            inv._slots[slotID].itemID = aItemID;
            inv._slots[slotID].itemMaxStack = aItemMaxStack;
            inv._slots[slotID].amount = dragAmount;
            inv._slots[slotID].titleText.text = inv._slots[slotID].itemName;
            inv._slots[slotID].amountTitle.text = inv._slots[slotID].amount.ToString();
            
            aName = bName;
            aItemID = bItemID;
            aItemMaxStack = bItemMaxStack;
            dragAmount = bAmount;
            
            dragCursor.setDragCursor(dragAmount);            
        }
        
    }
}