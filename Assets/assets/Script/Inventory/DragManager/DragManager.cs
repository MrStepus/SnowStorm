using UnityEngine;

namespace assets.Script.Inventory.DragManager
{
    public class DragManager : MonoBehaviour
    {
        public int aAmount;
        public int bAmount;

        public string aName;
        public string bName;
        
        public int aItemID;
        public int bItemID;
        
        public int itemMaxStack;
        
        public int aSlotID;
        public int bSlotID;

        public void ItemDragSlot(int amount)
        {
            
            
            aAmount += amount;
            
            
            
        }

    }
}