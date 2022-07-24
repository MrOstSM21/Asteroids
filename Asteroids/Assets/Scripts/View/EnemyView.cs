using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.View
{
    public class EnemyView : MonoBehaviour
    {
        public event Action SetMove;

        [SerializeField] Transform _transform;

        public Transform GetTransform => _transform;

        private void Update()
        {
            SetMove?.Invoke();
            
        }
    }
}