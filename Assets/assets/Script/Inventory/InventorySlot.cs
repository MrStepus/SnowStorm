using assets.Script.Inventory.DragManager;
using TMPro;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    
    public int itemID;
    public int amount = 0;
    public int itemMaxStack = 18;
    public string itemName;

    public int slotId;
    
    public TMP_Text titleText;
    public TMP_Text amountTitle;
    
    public DragManager dragManager;

    private void Start()
    {
        titleText.text = itemName;
        amountTitle.text = amount.ToString();
    }
    
    
    
    public void AddItem(int advancedItemId, int advancedAmount, string advancedItemName)
    {
        if (amount == 0)
        {
                   itemID = advancedItemId;
                   amount += advancedAmount;
                   itemName = advancedItemName;
                   
                   titleText.text = itemName;
                   amountTitle.text = amount.ToString(); 
        }
        else
        {
            amount += advancedAmount;
            amountTitle.text = amount.ToString(); 
        }
        
    }

    public void RemoveItem(int removeAmount)
    {
        
        amount -= removeAmount;
        
        if (amount == 0)
        {
            amountTitle.text = " ";
            titleText.text = " ";
            itemName = null;
            itemID = 0;
        }
        else
        { 
            amountTitle.text = amount.ToString();
        }
        
    }

    public void DragAndDropItem()
    {

        switch (dragManager.emptyDragCursor)
        {
            
            case true:
                dragManager.ItemDragSlot(amount, itemName, itemID, itemMaxStack, slotId);                
                break;
            
            case false:
                dragManager.ItemDropSlot(amount, itemName, itemID, itemMaxStack, slotId);
                break;
            
        }
        
    }
    
    
}
