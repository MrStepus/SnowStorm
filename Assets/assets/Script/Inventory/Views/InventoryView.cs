using TMPro;
using UnityEngine;

namespace assets.Script.Inventory.Views
{
    public class InventoryView : MonoBehaviour
    {
        
        [SerializeField] private InventorySlotView[] _slots;
        [SerializeField] private TMP_Text _textOwner;

        public string ownerId
        {
            get => _textOwner.text;
            set => _textOwner.text = value;
        }

        public InventorySlotView GetInventorySlotView(int index)
        {
            return _slots[index];
        }

    }
}