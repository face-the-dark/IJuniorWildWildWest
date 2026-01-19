using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour
{
    private PlayerInput _playerInput;

    public event Action<Vector2> Moved;
    public event Action<Vector2> Looked;
    public event Action<bool> Aimed;
    public event Action<bool> Shoot;
    
    private void Awake()
    {
        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.Enable();

        _playerInput.Player.Move.performed += OnMove;
        _playerInput.Player.Move.canceled += OnMove;
        
        _playerInput.Player.Look.performed += OnLook;
        _playerInput.Player.Shoot.performed += OnFire;
        
        _playerInput.Player.Aim.performed += OnAim;
        _playerInput.Player.Aim.canceled += OnAim;
    }

    private void OnDisable()
    {
        _playerInput.Player.Move.performed -= OnMove;
        _playerInput.Player.Move.canceled -= OnMove;
        
        _playerInput.Player.Look.performed -= OnLook;
        _playerInput.Player.Shoot.performed -= OnFire;
        
        _playerInput.Player.Aim.performed -= OnAim;
        _playerInput.Player.Aim.canceled -= OnAim;
        
        _playerInput.Disable();
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

    private void OnFire(InputAction.CallbackContext context)
    {
        bool isShoot = context.ReadValueAsButton();

        Shoot?.Invoke(isShoot);        
    }

    private void OnAim(InputAction.CallbackContext context)
    {
        bool isAimed = context.ReadValueAsButton();

        Aimed?.Invoke(isAimed);
    }
}
