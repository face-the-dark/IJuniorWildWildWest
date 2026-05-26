using Game.Gameplay.Features;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private Slider _slider;
        
        private void Start() => 
            UpdateSliderValue(_health.MaxValue);

        private void OnEnable()
        {
            _health.DamageTaken += OnDamageTaken;
            _health.Died += OnDied;
        }

        private void OnDisable()
        {
            _health.DamageTaken -= OnDamageTaken;
            _health.Died -= OnDied;
        }

        private void OnDamageTaken(float currentValue) => 
            UpdateSliderValue(currentValue);

        private void UpdateSliderValue(float currentValue) => 
            _slider.value = currentValue / _health.MaxValue;

        private void OnDied() => 
            _slider.gameObject.SetActive(false);
    }
}