using UnityEngine;

namespace Game.Scripts.Spawn
{
    public abstract class SpawnArea : MonoBehaviour
    {
        public abstract Vector3 GetRandomPosition();
    }
}