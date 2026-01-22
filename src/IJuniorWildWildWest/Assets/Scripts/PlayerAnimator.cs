using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private static readonly int IsWalkKey = Animator.StringToHash("IsWalk");
    private static readonly int IsAiming = Animator.StringToHash("IsAiming");
    private static readonly int VerticalKey = Animator.StringToHash("Vertical");
    private static readonly int HorizontalKey = Animator.StringToHash("Horizontal");
    private static readonly int ShootKey = Animator.StringToHash("Shoot");

    [SerializeField] private PlayerInputReader _inputReader;
    
    private Animator _animator;

    private Vector2 _direction;
    private bool _isAimed;

    private void Awake() => 
        _animator =  GetComponent<Animator>();

    private void OnEnable()
    {
        _inputReader.Moved += OnMoved;
        _inputReader.Aimed += OnAimed;
        _inputReader.Shoot += OnShoot;
    }

    private void OnDisable()
    {
        _inputReader.Moved -= OnMoved;
        _inputReader.Aimed -= OnAimed;
        _inputReader.Shoot -= OnShoot;
    }

    private void OnMoved(Vector2 direction)
    {
        _direction = direction;

        SwitchWalk();
    }

    private void OnAimed(bool isAimed)
    {
        _isAimed = isAimed;
        _animator.SetBool(IsAiming, isAimed);
        
        SwitchWalk();
    }

    private void OnShoot()
    {
        if (_isAimed)
            _animator.SetTrigger(ShootKey);
    }

    private void SwitchWalk()
    {
        if (_isAimed)
        {
            _animator.SetBool(IsWalkKey, false);
            _animator.SetFloat(HorizontalKey, _direction.x);
            _animator.SetFloat(VerticalKey, _direction.y);
        }
        else
        {
            _animator.SetBool(IsWalkKey, _direction.x != 0 || _direction.y != 0);
        }
    }
}