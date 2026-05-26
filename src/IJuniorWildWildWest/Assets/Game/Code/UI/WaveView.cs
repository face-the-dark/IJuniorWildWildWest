using Game.Infrastructure.Services;
using TMPro;
using UnityEngine;
using VContainer;

namespace Game.UI
{
    public class WaveView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        
        private WaveService _waveService;

        [Inject]
        public void Construct(WaveService waveService)
        {
            _waveService = waveService;
            _waveService.WaveChanged += OnCountIncreased;
        }

        private void OnDestroy()
        {
            _waveService.WaveChanged -= OnCountIncreased;
        }

        private void OnCountIncreased(string currentWave) => 
            _text.text = currentWave;
    }
}