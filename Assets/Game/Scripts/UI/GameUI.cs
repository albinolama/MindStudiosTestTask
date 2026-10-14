using Game.Scripts.Waves;
using TMPro;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI enemiesLeftTextMesh;
        [SerializeField] private TextMeshProUGUI currentWaveTextMesh;
        [SerializeField] private TextMeshProUGUI gameCompletedTextMesh;
    
        private WaveController _waveController;

        private void Start()
        {
            OnEnemyCountChanged(_waveController.EnemyCount);
            OnCurrentWaveChanged(_waveController.CurrentWave);
            gameCompletedTextMesh.enabled = false;
        }
    
        public void Initialize(WaveController waveController)
        {
            _waveController = waveController;

            _waveController.EnemyCountChanged += OnEnemyCountChanged;
            _waveController.CurrentWaveChanged += OnCurrentWaveChanged;
            _waveController.GameCompleted += OnGameCompleted;
        }

        private void OnEnemyCountChanged(int count) => enemiesLeftTextMesh.SetText("Enemies left: {0}", count);
        private void OnCurrentWaveChanged(int currentWave) => currentWaveTextMesh.SetText("Current wave: {0}", currentWave);

        private void OnGameCompleted()
        {
            currentWaveTextMesh.enabled = false;
            enemiesLeftTextMesh.enabled = false;
            gameCompletedTextMesh.enabled = true;
        }

        private void OnDestroy()
        {
            if (_waveController is null)
            {
                return;
            }
        
            _waveController.EnemyCountChanged -= OnEnemyCountChanged;
            _waveController.CurrentWaveChanged -= OnCurrentWaveChanged;
            _waveController.GameCompleted -= OnGameCompleted;
        }
    }
}