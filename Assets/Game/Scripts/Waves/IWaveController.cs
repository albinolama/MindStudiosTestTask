using System;

namespace Game.Scripts.Waves
{
    public interface IWaveController : IReadOnlyWaveController
    {
        event Action SpawnRequested;
        
        void RegisterSpawn();
        void RegisterDeath();
    }
    
    public interface IReadOnlyWaveController
    {
        int CurrentWave { get; }
        int EnemyCount { get; }

        event Action<int> CurrentWaveChanged;
        event Action<int> EnemyCountChanged;
        event Action GameCompleted;
    }
}