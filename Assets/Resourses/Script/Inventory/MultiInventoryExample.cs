using UnityEngine;
using assets.Script.Inventory;

public class MultiInventoryExample : MonoBehaviour
{
    [Header("Ссылки на инвентари")]
    [Tooltip("Инвентарь игрока")]
    public InventoryManager playerInventory;
    
    [Tooltip("Инвентарь сундука")]
    public InventoryManager chestInventory;

    [Header("Настройки")]
    public string currentOpenInventory = "Player";

    private void Start()
    {
        // ВАЖНО: Сначала регистрируем все инвентари в сервисе
        RegisterAllInventories();
        
        // По умолчанию открываем инвентарь игрока
        SimpleInventoryService.Instance.OpenInventory("Player");
        
        // Примеры добавления предметов в разные инвентари
        AddExampleItems();
    }

    private void Update()
    {
        // Горячие клавиши для переключения между инвентарями
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Открыть инвентарь игрока
            SimpleInventoryService.Instance.OpenInventory("Player");
            currentOpenInventory = "Player";
            Debug.Log("Открыт инвентарь ИГРОКА");
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // Открыть инвентарь сундука
            SimpleInventoryService.Instance.OpenInventory("Chest");
            currentOpenInventory = "Chest";
            Debug.Log("Открыт инвентарь СУНДУКА");
        }

        // Закрыть все инвентари
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SimpleInventoryService.Instance.CloseAllInventories();
            Debug.Log("Все инвентари закрыты");
        }

        // Примеры работы с предметами
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (string.IsNullOrEmpty(currentOpenInventory))
            {
                Debug.LogWarning("Сначала открой инвентарь (клавиши 1, 2, 3)");
                return;
            }
            // Добавить предмет в текущий открытый инвентарь
            SimpleInventoryService.Instance.AddItemToInventory(currentOpenInventory, 26050202, 5);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            // Удалить предмет из текущего инвентаря
            SimpleInventoryService.Instance.RemoveItemFromInventory( currentOpenInventory, 1, 2);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            // Проверить количество предмета в инвентаре игрока
            int amount = SimpleInventoryService.Instance.GetItemAmount("Player", 1);
            Debug.Log($"У игрока зелий здоровья: {amount}");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            // Проверить, есть ли у игрока 10 зелий
            bool hasEnough = SimpleInventoryService.Instance.HasItem("Player", 1, 10);
            Debug.Log($"У игрока есть 10+ зелий? {hasEnough}");
        }
    }

    /// <summary>
    /// Регистрируем все инвентари в сервисе
    /// </summary>
    private void RegisterAllInventories()
    {
        if (SimpleInventoryService.Instance == null)
        {
            Debug.LogError("SimpleInventoryService не найден! Создайте пустой объект и добавьте компонент.");
            return;
        }

        // Регистрируем инвентарь игрока
        if (playerInventory != null)
        {
            SimpleInventoryService.Instance.RegisterInventory("Player", playerInventory);
        }

        // Регистрируем инвентарь сундука
        if (chestInventory != null)
        {
            SimpleInventoryService.Instance.RegisterInventory("Chest", chestInventory);
        }
    }
    
    /// Примеры добавления предметов в разные инвентари
    private void AddExampleItems()
    {
        // Добавляем предметы игроку
        SimpleInventoryService.Instance.AddItemToInventory("Player", 26110402, 15);
        SimpleInventoryService.Instance.AddItemToInventory("Player", 26110502, 8);
        
        // Добавляем предметы в сундук
        SimpleInventoryService.Instance.AddItemToInventory("Chest", 26110402, 18);
        SimpleInventoryService.Instance.AddItemToInventory("Chest", 26050302, 3);
    }
    
    /// Пример: перенести предмет из одного инвентаря в другой
    public void TransferItem(string fromOwnerId, string toOwnerId, int itemId, int amount, string itemName)
    {
        // Проверяем, есть ли предмет у отправителя
        if (!SimpleInventoryService.Instance.HasItem(fromOwnerId, itemId, amount))
        {
            Debug.LogWarning($"У {fromOwnerId} недостаточно предметов для передачи!");
            return;
        }

        // Удаляем из первого инвентаря
        SimpleInventoryService.Instance.RemoveItemFromInventory(fromOwnerId, itemId, amount);
        
        // Добавляем во второй инвентарь
        SimpleInventoryService.Instance.AddItemToInventory(toOwnerId, itemId, amount);
        
        Debug.Log($"Передано {amount}x {itemName} из {fromOwnerId} в {toOwnerId}");
    }
}
