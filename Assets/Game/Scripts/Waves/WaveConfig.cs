using System;
using UnityEngine;

namespace Game.Scripts.Waves
{
    [Serializable]
    public struct WaveConfig
    {
        [Min(1)] public int enemyCount;
        [Min(0)] public float enemySpawnInterval;
        [Min(0)] public float nextWaveDelay;
    }
}