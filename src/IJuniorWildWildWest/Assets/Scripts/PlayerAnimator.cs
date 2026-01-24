using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private static readonly int IsRunKey = Animator.StringToHash("IsRun");
    private static readonly int IsAimKey = Animator.StringToHash("IsAim");
    private static readonly int VerticalKey = Animator.StringToHash("Vertical");
    private static readonly int HorizontalKey = Animator.StringToHash("Horizontal");
    private static readonly int FireKey = Animator.StringToHash("Fire");

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
        _animator.SetBool(IsAimKey, isAimed);
        
        SwitchWalk();
    }

    private void OnShoot()
    {
        if (_isAimed)
            _animator.SetTrigger(FireKey);
    }

    private void SwitchWalk()
    {
        if (_isAimed)
        {
            _animator.SetBool(IsRunKey, false);
            _animator.SetFloat(HorizontalKey, _direction.x);
            _animator.SetFloat(VerticalKey, _direction.y);
        }
        else
        {
            _animator.SetBool(IsRunKey, _direction.x != 0 || _direction.y != 0);
        }
    }
}