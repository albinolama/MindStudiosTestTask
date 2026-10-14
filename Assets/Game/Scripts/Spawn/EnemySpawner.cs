using Game.Scripts.Views;
using Game.Scripts.Waves;
using UnityEngine;

namespace Game.Scripts.Spawn
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyView enemyPrefab;
        [SerializeField] private SpawnArea spawnArea;
        
        private WaveController _waveController;
    
        public void Initialize(WaveController waveController)
        {
            _waveController = waveController;
            _waveController.SpawnRequested += SpawnEnemy;
        }

        private void SpawnEnemy()
        {
            var pos = spawnArea.GetRandomPosition();
            var enemy = Instantiate(enemyPrefab, pos, Quaternion.identity);
            enemy.Died += OnEnemyDied;
            _waveController.RegisterSpawn();
        }

        private void OnEnemyDied(EnemyView enemy)
        {
            enemy.Died -= OnEnemyDied;
            _waveController.RegisterDeath();
        }

        private void OnDestroy()
        {
            if (_waveController is not null)
            {
                _waveController.SpawnRequested -= SpawnEnemy;
            }
        }
    }
}