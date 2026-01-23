using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour
{
    private PlayerInput _input;

    public event Action<Vector2> Moved;
    public event Action<Vector2> Looked;
    public event Action<bool> Aimed;
    public event Action Shoot;
    
    private void Awake() => 
        _input = new PlayerInput();

    private void OnEnable()
    {
        _input.Enable();

        _input.Player.Move.performed += OnMove;
        _input.Player.Move.canceled += OnMove;
        
        _input.Player.Look.performed += OnLook;
        _input.Player.Shoot.performed += OnShoot;
        
        _input.Player.Aim.performed += OnAim;
        _input.Player.Aim.canceled += OnAim;
    }

    private void OnDisable()
    {
        _input.Player.Move.performed -= OnMove;
        _input.Player.Move.canceled -= OnMove;
        
        _input.Player.Look.performed -= OnLook;
        _input.Player.Shoot.performed -= OnShoot;
        
        _input.Player.Aim.performed -= OnAim;
        _input.Player.Aim.canceled -= OnAim;
        
        _input.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        
        Moved?.Invoke(direction);
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();

        Looked?.Invoke(direction);
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        Shoot?.Invoke();        
    }

    private void OnAim(InputAction.CallbackContext context)
    {
        bool isAimed = context.ReadValueAsButton();

        Aimed?.Invoke(isAimed);
    }
}
