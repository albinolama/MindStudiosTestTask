using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Waves
{
    [CreateAssetMenu(menuName = "Game/Wave Sequence")]
    public class WaveSequence : ScriptableObject
    {
        [SerializeField] private List<WaveConfig> waves = new();
        public IReadOnlyList<WaveConfig> Waves => waves;

        [SerializeField] private bool isEndless;
        public bool IsEndless => isEndless;

        [SerializeField] private WaveConfig endlessModeWave;
        public WaveConfig EndlessModeWave => endlessModeWave;
    }
}