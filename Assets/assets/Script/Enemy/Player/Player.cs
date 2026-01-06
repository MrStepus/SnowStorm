using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Vector3 _direction;

    private void OnEnable()
    {
        InputManager.Instance.actions.Player.Move.performed += OnMove;
        InputManager.Instance.actions.Player.Move.canceled += OnMoveStop;
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
}

