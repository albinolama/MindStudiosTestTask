using System;
using UnityEngine;

namespace Game.Scripts.Views
{
    [RequireComponent(typeof(Collider))]
    public class EnemyView : MonoBehaviour
    {
        public event Action<EnemyView> Died;
        private bool _isDead;
    
        private void OnMouseDown()
        {
            if (_isDead)
            {
                return;
            }

            _isDead = true;
        
            Died?.Invoke(this);
        
            Destroy(gameObject);
        }
    }
}