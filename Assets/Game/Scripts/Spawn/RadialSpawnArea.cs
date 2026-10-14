using UnityEditor;
using UnityEngine;

namespace Game.Scripts.Spawn
{
    public class RadialSpawnArea : SpawnArea
    {
        [SerializeField, Min(0f)] private float innerRadius = 2f;
        [SerializeField, Min(0f)] private float outerRadius = 5f;
        
        [SerializeField] private bool drawInnerRadius = true;
        [SerializeField] private bool drawOuterRadius = true;
        
        private const float TwoPi = Mathf.PI * 2f;

        private void OnValidate()
        {
            innerRadius = Mathf.Clamp(innerRadius, 0f, outerRadius);
            outerRadius = Mathf.Max(outerRadius, innerRadius);
        }
        
        public override Vector3 GetRandomPosition()
        {
            var inner = Mathf.Min(innerRadius, outerRadius);
            var outer = Mathf.Max(innerRadius, outerRadius);

            var angle = Random.Range(0f, TwoPi);

            var radius = Mathf.Sqrt(Mathf.Lerp(inner * inner, outer * outer, Random.value));

            Vector3 offset = new(Mathf.Cos(angle) * radius, 0f, Mathf.Sin(angle) * radius);

            return transform.TransformPoint(offset);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (drawInnerRadius)
            {
                using (new Handles.DrawingScope(Color.green, transform.localToWorldMatrix))
                {
                    Handles.DrawWireDisc(Vector3.zero, Vector3.up, innerRadius);
                } 
            }

            if (!drawOuterRadius)
            {
                return;
            }
            
            using (new Handles.DrawingScope(Color.blue, transform.localToWorldMatrix))
            {
                Handles.DrawWireDisc(Vector3.zero, Vector3.up, outerRadius);
            }
        }
#endif
    }
}