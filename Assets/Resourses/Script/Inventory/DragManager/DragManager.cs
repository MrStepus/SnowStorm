using UnityEngine;
using assets.Script.Inventory;

namespace assets.Script.Inventory.DragManager
{
    public class DragManager : MonoBehaviour
    {
        [Header("Ссылки")]
        public DragCursor dragCursor;

        [Header("Состояние")]
        public bool emptyDragCursor = true;
        
        // ===== Запоминаем из какого инвентаря взяли предмет =====
        private string sourceOwnerId; // ID инвентаря, откуда взяли предмет
        
        // Данные перетаскиваемого предмета (слот A)
        public int dragAmount;
        public int aItemID = 11111111;
        public int aSlotID;

        // Временные данные для свапа (слот B)
        private int bAmount;
        private int bItemID;
        
        /// Вызывается когда берём предмет из слота
        public void ItemDragSlot(string ownerId, int amount, int itemID, int slotID)
        {
            // ФИКС: Проверяем что слот не пустой
            if (itemID == 11111111 || amount == 0)
            {
                Debug.LogWarning($"[DragManager] Попытка взять пустой слот {slotID} из '{ownerId}'");
                return;
            }
            
            emptyDragCursor = false;
            
            // Запоминаем данные предмета
            sourceOwnerId = ownerId;
            dragAmount = amount;
            aItemID = itemID;
            aSlotID = slotID;
            
            // Получаем нужный инвентарь через сервис
            var inventory = SimpleInventoryService.Instance.GetInventory(ownerId);
            if (inventory == null)
            {
                Debug.LogError($"[DragManager] Инвентарь '{ownerId}' не найден!");
                emptyDragCursor = true; // Сбрасываем состояние
                return;
            }

            // ФИКС: Проверяем валидность индекса слота
            if (slotID < 0 || slotID >= inventory._slots.Count)
            {
                Debug.LogError($"[DragManager] Некорректный slotID {slotID} для инвентаря '{ownerId}' (всего слотов: {inventory._slots.Count})");
                emptyDragCursor = true;
                return;
            }

            // Очищаем слот
            var slot = inventory._slots[slotID];
            slot.itemID = 11111111;
            slot.amount = 0;
            slot.UpdateUI();

            // Показываем курсор
            if (dragAmount != 0)
            {
                dragCursor.setDragCursor(dragAmount);
            }

            var config = ItemDatabase.GetConfig(aItemID);
            Debug.Log($"[DragManager] Взяли предмет '{config.displayName}' из инвентаря '{ownerId}', слот {slotID}");
        }
        
        /// Вызывается когда кладём предмет в слот
        public void ItemDropSlot(string targetOwnerId, int slotID)
        {
            
            if (emptyDragCursor)
            {
                Debug.LogWarning("[DragManager] Попытка положить предмет, но руки пусты!");
                return;
            }
            
            // Получаем целевой инвентарь через сервис
            var targetInventory = SimpleInventoryService.Instance.GetInventory(targetOwnerId);
            if (targetInventory == null)
            {
                Debug.LogError($"[DragManager] Целевой инвентарь '{targetOwnerId}' не найден!");
                return;
            }

            // ФИКС: Проверяем валидность индекса
            if (slotID < 0 || slotID >= targetInventory._slots.Count)
            {
                Debug.LogError($"[DragManager] Некорректный slotID {slotID} для инвентаря '{targetOwnerId}'");
                return;
            }

            var targetSlot = targetInventory._slots[slotID];
            var conf = ItemDatabase.GetConfig(aItemID);
            // Случай 1: Слот пустой - просто кладём предмет

            if (targetSlot.itemID == 11111111 && targetSlot.slotType ==  conf.itemType || targetSlot.slotType == "any")

            {
                targetSlot.AddItem(aItemID, dragAmount);
                emptyDragCursor = true;
                dragCursor.clearDragCursor();
                aItemID = 11111111;
                dragAmount = 0;
                var config = ItemDatabase.GetConfig(aItemID);
                Debug.Log($"[DragManager] Положили предмет '{config.displayName}' в '{targetOwnerId}', слот {slotID}");
            }
            // Случай 2: В слоте тот же предмет - складываем стаки
            else if (targetSlot.itemID == aItemID)
            {
                int freeSpace = conf.maxStack - targetSlot.amount;
                if (freeSpace <= 0)
                {
                    if (targetSlot.slotType == conf.itemType || targetSlot.slotType == "any" || conf.itemType == "all")
                    {
                        ItemSwap(targetOwnerId, targetSlot, slotID);                    
                    }
                }
                else
                {
                    StackItems(targetOwnerId, slotID);                    
                }
            }
            // Случай 3: В слоте другой предмет - делаем обмен (swap)
            else
            {
                if (conf == null)
                {
                    Debug.LogError($"Config не найден для itemID {aItemID}");
                    return;
                }

                if (targetSlot.slotType == conf.itemType || targetSlot.slotType == "any")
                {
                    ItemSwap(targetOwnerId, targetSlot, slotID);                    
                }
            }
        }


        /// Обмен предметов между слотами (возможно между разными инвентарями!)
        private void ItemSwap(string targetOwnerId, InventorySlot targetSlot, int slotID)
        {
            // Запоминаем данные предмета из целевого слота
            bItemID = targetSlot.itemID;
            bAmount = targetSlot.amount;

            // Кладём наш предмет в целевой слот
            targetSlot.itemID = aItemID;
            targetSlot.amount = dragAmount;
            targetSlot.UpdateUI();

            // Теперь в руках у нас предмет из целевого слота
            aItemID = bItemID;
            dragAmount = bAmount;
            sourceOwnerId = targetOwnerId; // ВАЖНО: обновляем источник!

            dragCursor.setDragCursor(dragAmount);
            if (aItemID == 0)
            {
                aItemID = 11111111; 
                dragCursor.clearDragCursor();
            }
            
            var config = ItemDatabase.GetConfig(aItemID);
            if (config != null)
            {
                Debug.Log($"[DragManager] Обменяли, в руке: '{config.displayName}'");
            }
            else
            {
                Debug.Log($"[DragManager] Обменяли, в руке: ID={aItemID}");
            }
            Debug.Log($"[DragManager] Обменяли предметы в инвентаре '{targetOwnerId}', теперь в руке: '{config.displayName}'");
        }
        
        /// Складываем одинаковые предметы в стак
        private void StackItems(string targetOwnerId, int slotID)
        {
            var targetInventory = SimpleInventoryService.Instance.GetInventory(targetOwnerId);
            var targetSlot = targetInventory._slots[slotID];
            var config = ItemDatabase.GetConfig(aItemID);
            
            // Сколько можно добавить в слот
            int spaceInSlot = config.maxStack - targetSlot.amount;

            if (spaceInSlot >= dragAmount)
            {
                // Весь стак влезает
                targetSlot.amount += dragAmount;
                targetSlot.amountTitle.text = targetSlot.amount.ToString();
                
                emptyDragCursor = true;
                
                Debug.Log($"[DragManager] Сложили {dragAmount}x '{config.displayName}' в стак в '{targetOwnerId}', слот {slotID}");
                dragCursor.clearDragCursor();
            }
            else if (spaceInSlot > 0)
            {
                // Частично влезает
                targetSlot.amount = config.maxStack; // Заполняем слот до максимума
                targetSlot.amountTitle.text = targetSlot.amount.ToString();
                
                dragAmount -= spaceInSlot; // Остаток остаётся в руке
                dragCursor.setDragCursor(dragAmount);
                
                Debug.Log($"[DragManager] Частично сложили предметы, добавлено {spaceInSlot}, осталось в руке: {dragAmount}");
            }
            else
            {
                Debug.LogWarning($"[DragManager] Стак в слоте {slotID} уже полон!");
            }
        }
        
        /// ОПЦИОНАЛЬНОЕ: Сбросить предмет обратно в исходный слот (например, по правой кнопке мыши)
        public void CancelDrag()
        {
            var conf = ItemDatabase.GetConfig(aItemID);
            if (emptyDragCursor) return;

            var sourceInventory = SimpleInventoryService.Instance.GetInventory(sourceOwnerId);
            if (sourceInventory != null && aSlotID >= 0 && aSlotID < sourceInventory._slots.Count)
            {
                var sourceSlot = sourceInventory._slots[aSlotID];
                sourceSlot.AddItem(aItemID, dragAmount);
                
                Debug.Log($"[DragManager] Вернули предмет '{conf.displayName}' обратно в '{sourceOwnerId}', слот {aSlotID}");
            }

            emptyDragCursor = true;
            dragCursor.clearDragCursor();
        }
        
        /// ОПЦИОНАЛЬНОЕ: Сбросить предмет "в мир" (удалить)
        public void DropItemToWorld()
        {
            var conf = ItemDatabase.GetConfig(aItemID);
            if (emptyDragCursor) return;

            Debug.Log($"[DragManager] Выбросили предмет '{conf.displayName}' x{dragAmount} в мир");
            
            // Здесь можно создать объект в мире, например:
            // Instantiate(itemPrefab, playerPosition, Quaternion.identity);
            
            emptyDragCursor = true;
            dragCursor.clearDragCursor();
        }
    }
}