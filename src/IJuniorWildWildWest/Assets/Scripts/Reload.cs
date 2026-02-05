using System;
using UnityEngine;

[Serializable]
public class Reload
{
    [SerializeField] private float _value;

    private float _timesUp;
    
    public bool IsExpired => _timesUp <= Time.time;

    public void Reset() => 
        _timesUp = Time.time + _value;
}