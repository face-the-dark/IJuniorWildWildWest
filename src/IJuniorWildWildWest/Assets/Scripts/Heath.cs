using System;
using UnityEngine;

public class Heath : MonoBehaviour
{
    [SerializeField] private float _value = 100f;

    public event Action<float> DamageTaken;
    
    public void TakeDamage(float damage)
    {
        if (damage > 0)
        {
            _value -= damage;
            
            DamageTaken?.Invoke(_value);
            Debug.Log(_value);
        }
    }
}