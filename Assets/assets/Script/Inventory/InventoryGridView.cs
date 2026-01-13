using UnityEngine;

public class InventoryGridView : MonoBehaviour
{
    public void Setup(IReadOnlyInventoryGrid inventory)
    {
        var slots = inventory.GetSlots();
        var size = inventory.Size;

        for (var x = 0; x < size.x; x++)
        {
            for (var y = 0; y < size.y; y++)
            {
                var slot = slots[x, y];

                Debug.Log($"Slot ({x}, {y}). Item: {slot.itemId}, amount: {slot.amount} ");
            }
        }

    }
}
