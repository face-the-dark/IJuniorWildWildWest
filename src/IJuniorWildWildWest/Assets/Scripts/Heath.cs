using System;
using UnityEngine;

public class Heath : MonoBehaviour
{
    private const float MinValue = 0f;
    
    [SerializeField] private float _maxValue = 100f;
    
    private float _currentValue;
    private bool _isDead;
    
    public event Action<float> DamageTaken;
    public event Action Died;

    private void Start() => 
        _currentValue = _maxValue;

    public void TakeDamage(float damage)
    {
        if (_isDead) 
            return;
        
        if (damage > 0)
        {
            _currentValue -= damage;
            
            DamageTaken?.Invoke(_currentValue);

            TryDead();
        }
    }

    private void TryDead()
    {
        if (_isDead)
            return;
        
        if (_currentValue <= MinValue)
        {
            _isDead = true;
            
            Died?.Invoke();
        }
    }
}