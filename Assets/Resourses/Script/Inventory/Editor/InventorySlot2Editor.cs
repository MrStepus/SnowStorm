#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InventorySlot))]
public class InventorySlotEditor : Editor
{
    // Список доступных типов слотов
    private static readonly string[] slotTypes = new string[]
    {
        "any",
        "food",
        "weapon",
        "resources",
        "shells"
    };

    public override void OnInspectorGUI()
    {
        // Получаем ссылку на объект
        InventorySlot slot = (InventorySlot)target;

        // Рисуем все стандартные поля
        DrawDefaultInspector();

        // Добавляем пространство
        EditorGUILayout.Space();
        
        // Создаём label
        EditorGUILayout.LabelField("Выбор типа слота", EditorStyles.boldLabel);

        // Находим индекс текущего типа
        int currentIndex = System.Array.IndexOf(slotTypes, slot.slotType);
        if (currentIndex == -1) currentIndex = 0; // Если не найден - ставим Any

        // Рисуем dropdown
        int newIndex = EditorGUILayout.Popup("Тип слота", currentIndex, slotTypes);

        // Если выбор изменился - обновляем значение
        if (newIndex != currentIndex)
        {
            Undo.RecordObject(slot, "Change Slot Type");
            slot.slotType = slotTypes[newIndex];
            EditorUtility.SetDirty(slot);
        }

        // Показываем текущее значение для справки
        EditorGUILayout.HelpBox($"Текущий тип: {slot.slotType}", MessageType.Info);
    }
}
#endif