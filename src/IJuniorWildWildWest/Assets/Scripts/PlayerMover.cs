using UnityEngine;

[RequireComponent(typeof(PlayerInputReader))]
[RequireComponent(typeof(CharacterController))]
public class PlayerMover : PlayerTransformer
{
    private CharacterController _characterController;

    protected override void Awake()
    {
        base.Awake();
        
        _characterController = GetComponent<CharacterController>();
    }

    protected override void Transform(Vector3 direction)
    {
        direction *= Speed * Time.deltaTime;
        
        _characterController.Move(direction);
    }
}