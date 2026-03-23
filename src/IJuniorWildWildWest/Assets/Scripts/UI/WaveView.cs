using TMPro;
using UnityEngine;

namespace UI
{
    public class WaveView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private WaveRunner _waveRunner;
        
        private void OnEnable() => 
            _waveRunner.CountIncreased += OnCountIncreased;

        private void OnDisable() => 
            _waveRunner.CountIncreased -= OnCountIncreased;

        private void OnCountIncreased(int currentWave) => 
            _text.text = $"{currentWave} / {_waveRunner.WaveCount}";
    }
}