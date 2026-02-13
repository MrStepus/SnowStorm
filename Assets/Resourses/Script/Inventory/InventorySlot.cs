using assets.Script.Inventory.DragManager;
using TMPro;
using UnityEngine;
public class InventorySlot : MonoBehaviour
{
    [Header("Данные предмета")]
    public int itemID = 11111111;
    public int amount = 0;

    [Header("Данные слота")]
    public int slotId;
    public string slotType;
    
    [Header("UI")]
    public TMP_Text titleText;
    public TMP_Text amountTitle;
    
    public DragManager dragManager;
    
    public InventoryManager parentInventory;

    private void Start()
    {
        UpdateUI();
    }
    
    /// Добавить предмет в слот
    public void AddItem(int advancedItemId, int advancedAmount)
    {
        if (amount == 0)
        {
            itemID = advancedItemId;
            amount = advancedAmount;
        }
        else
        {
            amount += advancedAmount;
        }
        
        UpdateUI();
    }
    
    public void RemoveItem(int removeAmount)
    {
        amount -= removeAmount;
        
        if (amount <= 0)
        {
            ClearSlot();
        }
        
        UpdateUI();
    }
    
    /// Очистить слот полностью
    public void ClearSlot()
    {
        itemID = 11111111;
        amount = 0;
        UpdateUI();
    }
    
    /// Обновить UI слота
    public void UpdateUI()
    {
        if (amount == 0 || itemID == 11111111)
        {
            titleText.text = "";
            amountTitle.text = "";
        }
        else
        {
            var config = ItemDatabase.GetConfig(itemID);
            titleText.text = config.displayName;
            amountTitle.text = amount.ToString();
        }
    }
    
    public void DragAndDropItem()
    {
        if (parentInventory == null)
        {
            Debug.LogError($"[InventorySlot] Слот {slotId} не привязан к инвентарю!");
            return;
        }
        
        if (dragManager == null)
        {
            Debug.LogError($"[InventorySlot] Слот {slotId} не имеет ссылки на DragManager!");
            return;
        }

        string ownerId = parentInventory.ownerId;

        if (dragManager.emptyDragCursor)
        {

            if (itemID == 11111111 || amount == 0)
            {
                Debug.Log($"[InventorySlot] Попытка взять пустой слот {slotId}");
                return;
            }
            
            dragManager.ItemDragSlot(ownerId, amount, itemID, slotId);
            UpdateUI();
        }
        else
        {
            dragManager.ItemDropSlot(ownerId, slotId);
            UpdateUI();
        }
    }
    
    /// ОПЦИОНАЛЬНОЕ: Проверить, пустой ли слот
    public bool IsEmpty()
    {
        return itemID == 11111111 || amount == 0;
    }
    
    /// ОПЦИОНАЛЬНОЕ: Проверить, можно ли добавить предмет в этот слот
    public bool CanAddItem(int checkItemId, int checkAmount)
    {
        // Если слот пустой - можем добавить
        if (IsEmpty()) return true;
        
        var conf = ItemDatabase.GetConfig(itemID);
        if (itemID == checkItemId && (amount + checkAmount) <= conf.maxStack) 
        {
            return true;
        }

        return false;
    }
    
    /// ОПЦИОНАЛЬНОЕ: Получить свободное место в слоте для этого предмета
    public int GetFreeSpace(int checkItemId)
    {
        var config = ItemDatabase.GetConfig(itemID);
        if (IsEmpty()) return config.maxStack;
        if (itemID != checkItemId) return 0;
        return config.maxStack - amount;
    }
}
