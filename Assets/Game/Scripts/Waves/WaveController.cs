using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Game.Scripts.Waves
{
    public class WaveController
    {
        public event Action SpawnRequested;
        public event Action GameCompleted;
    
        public int CurrentWave { get; private set; }
        public event Action<int> CurrentWaveChanged;
    
        public int EnemyCount { get; private set; }
        public event Action<int> EnemyCountChanged;
    
        private bool HasNextWave => _isEndless || CurrentWave < _waves.Count;
    
        private readonly IReadOnlyList<WaveConfig> _waves;
        private readonly bool _isEndless;
        private readonly WaveConfig _endlessWaveConfig;

        private bool _isRunning;

        public WaveController(IReadOnlyList<WaveConfig> waves, bool isEndless, WaveConfig endlessWaveConfig)
        {
            _waves = waves;
            _isEndless = isEndless;
            _endlessWaveConfig = endlessWaveConfig;
        }
    
        // ReSharper disable once CognitiveComplexity
        public async Awaitable RunAsync(CancellationToken token)
        {
            if (_isRunning)
            {
                Debug.LogWarning("Wave controller is already running");
                return;
            }
        
            _isRunning = true;
        
            try
            {
                while (TryGetNextWave(out var wave))
                {
                    for (var i = 0; i < wave.enemyCount; i++)
                    {
                        token.ThrowIfCancellationRequested();

                        SpawnRequested?.Invoke();

                        if (i < wave.enemyCount - 1)
                        {
                            await Awaitable.WaitForSecondsAsync(wave.enemySpawnInterval, token);
                        }
                    }

                    while (EnemyCount > 0)
                    {
                        await Awaitable.NextFrameAsync(token);
                    }

                    if (!HasNextWave)
                    {
                        break;
                    }

                    await Awaitable.WaitForSecondsAsync(wave.nextWaveDelay, token);
                }

                GameCompleted?.Invoke();
            }
            finally
            {
                _isRunning = false;
            }
        }
    
        private bool TryGetNextWave(out WaveConfig waveConfig)
        {
            if (CurrentWave < _waves.Count)
            {
                waveConfig = _waves[CurrentWave];
            }
            else if (_isEndless)
            {
                waveConfig = _endlessWaveConfig;
            }
            else
            {
                waveConfig = default;
                return false;
            }
        
            CurrentWave++;
            CurrentWaveChanged?.Invoke(CurrentWave);
            return true;
        }

        public void RegisterSpawn()
        {
            EnemyCount++;
            EnemyCountChanged?.Invoke(EnemyCount);
        }
    
        public void RegisterDeath()
        {
            EnemyCount--;
            EnemyCountChanged?.Invoke(EnemyCount);
        }
    }
}