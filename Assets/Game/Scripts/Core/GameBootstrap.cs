using System;
using Game.Scripts.Spawn;
using Game.Scripts.UI;
using Game.Scripts.Waves;
using UnityEngine;

namespace Game.Scripts.Core
{
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private WaveSequence waveSequence;
        [SerializeField] private GameUI gameUI;

        private WaveController _controller;

        private void Awake()
        {
            if (waveSequence == null || enemySpawner == null || gameUI == null)
            {
                Debug.LogError("Missing dependencies", this);
                return;
            }

            _controller = new WaveController(waveSequence.Waves, waveSequence.IsEndless, waveSequence.EndlessModeWave);
            enemySpawner.Initialize(_controller);
            gameUI.Initialize(_controller);
        }
    
        // ReSharper disable once AsyncVoidMethod
        private async void Start()
        {
            try
            {
                await _controller.RunAsync(destroyCancellationToken);
            }
            catch (OperationCanceledException) { }
        }
    }
}
