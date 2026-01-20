using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class DragCursor : MonoBehaviour
{
    public static DragCursor Instance { get; private set; }

    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _amountText;

    public string CurrentItemId { get; private set; }
    public int CurrentAmount { get; private set; }

    private void Awake() => Instance = this;
    
    
    public void SetData(string itemId, int amount, Sprite icon)
    {
        CurrentItemId = itemId;
        CurrentAmount = amount;
        _icon.sprite = icon;
        // Показываем текст, только если предметов > 1
        _amountText.text = amount > 1 ? amount.ToString() : "";
        gameObject.SetActive(true);
    }

    public void Clear()
    {
        CurrentItemId = null;
        CurrentAmount = 0;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        // Следуем за курсором через новую Input System
        transform.position = Mouse.current.position.ReadValue();
    }
}