using System.Collections.Generic;
using UnityEngine;

namespace assets.Script.Inventory
{
    /// Простой сервис для управления несколькими инвентарями
    /// Каждый инвентарь привязан к своему владельцу (ownerId)
    public class SimpleInventoryService : MonoBehaviour
    {
        public static SimpleInventoryService Instance { get; private set; }

        // Словарь всех инвентарей: ключ = ownerId, значение = InventoryManager
        private Dictionary<string, InventoryManager> _inventories = new Dictionary<string, InventoryManager>();

        private void Awake()
        {
            // Singleton паттерн - только один экземпляр сервиса
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            
            ItemDatabase.Load();
        }
        
        /// Регистрирует новый инвентарь в системе
        public void RegisterInventory(string ownerId, InventoryManager inventory)
        {
            if (_inventories.ContainsKey(ownerId))
            {
                Debug.LogWarning($"Инвентарь с ownerId '{ownerId}' уже зарегистрирован!");
                return;
            }

            _inventories[ownerId] = inventory;
            inventory.ownerId = ownerId; // Устанавливаем ownerId в самом инвентаре
            Debug.Log($"Инвентарь '{ownerId}' зарегистрирован!");
        }
        
        /// Получить инвентарь по ID владельца
        public InventoryManager GetInventory(string ownerId)
        {
            if (_inventories.TryGetValue(ownerId, out InventoryManager inventory))
            {
                return inventory;
            }

            Debug.LogError($"Инвентарь с ownerId '{ownerId}' не найден!");
            return null;
        }
        
        /// Добавить предмет в конкретный инвентарь
        public void AddItemToInventory(string ownerId, int itemId, int amount)
        {
            var inventory = GetInventory(ownerId);
            if (inventory != null)
            {
                inventory.AddItemForSlot(itemId, amount);
            }
        }
        
        /// Удалить предмет из конкретного инвентаря
        public void RemoveItemFromInventory(string ownerId, int itemId, int amount)
        {
            var inventory = GetInventory(ownerId);
            if (inventory != null)
            {
                inventory.RemoveItemForSlot(itemId, amount);
            }
        }
        
        /// Открыть/показать конкретный инвентарь
        public void OpenInventory(string ownerId)
        {
            // Сначала скрываем все инвентари
            foreach (var inv in _inventories.Values)
            {
                inv.gameObject.SetActive(false);
            }

            // Показываем нужный
            var inventory = GetInventory(ownerId);
            if (inventory != null)
            {
                inventory.gameObject.SetActive(true);
                Debug.Log($"Открыт инвентарь '{ownerId}'");
            }
        }
        
        /// Закрыть все инвентари
        public void CloseAllInventories()
        {
            foreach (var inv in _inventories.Values)
            {
                inv.gameObject.SetActive(false);
            }
        }
        
        /// Проверить, есть ли в инвентаре определённое количество предмета
        public bool HasItem(string ownerId, int itemId, int amount)
        {
            var inventory = GetInventory(ownerId);
            if (inventory == null) return false;

            int totalAmount = 0;
            foreach (var slot in inventory._slots)
            {
                if (slot.itemID == itemId)
                {
                    totalAmount += slot.amount;
                }
            }

            return totalAmount >= amount;
        }
        
        /// Получить общее количество предмета в инвентаре
        public int GetItemAmount(string ownerId, int itemId)
        {
            var inventory = GetInventory(ownerId);
            if (inventory == null) return 0;

            int totalAmount = 0;
            foreach (var slot in inventory._slots)
            {
                if (slot.itemID == itemId)
                {
                    totalAmount += slot.amount;
                }
            }

            return totalAmount;
        }
    }
}
