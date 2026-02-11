using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Vector3 _direction;
    private bool inventoryOpenend = false;
    public GameObject inventory;
    public GameObject dragCursor;

    private void OnEnable()
    {
        InputManager.Instance.actions.Player.Move.performed += OnMove;
        InputManager.Instance.actions.Player.Move.canceled += OnMoveStop;
        InputManager.Instance.actions.Player.Inventory.performed += OpenAndCloseInventory;
    }

    private void OnDisable()
    {
        InputManager.Instance.actions.Player.Move.performed -= OnMove;
        InputManager.Instance.actions.Player.Move.canceled -= OnMoveStop;
    }

    private void Update()
    {
        Move(_direction);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _direction = context.ReadValue<Vector3>();
         
    }

    public void OnMoveStop(InputAction.CallbackContext context)
    {
        _direction = Vector3.zero; 
    }

    private void Move(Vector2 directionMove)
    {
        Vector3 movement = new Vector3(_direction.x, 0f, _direction.z) * moveSpeed * Time.deltaTime;
        transform.position += movement;
    }

    private void OpenAndCloseInventory(InputAction.CallbackContext context)
    {
        switch (inventoryOpenend)
        {
            case false:
                inventory.SetActive(true); 
                inventoryOpenend = true;
                break;
            
            case true:
                inventory.SetActive(false);  
                dragCursor.SetActive(false);
                inventoryOpenend = false;
                break;
        }
    }
    
    
}

