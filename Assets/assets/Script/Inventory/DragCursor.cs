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

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.position = Mouse.current.position.ReadValue();
    }
    
    public void setDragCursor(int amount)
    {
        _amountText.text = amount.ToString();
        gameObject.SetActive(true);
    }

    public void clearDragCursor()
    {
        _amountText.text = "";
        gameObject.SetActive(false);
    }
    
}