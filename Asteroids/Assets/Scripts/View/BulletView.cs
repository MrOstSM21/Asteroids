using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.View
{
    public class BulletView : MonoBehaviour
    {
        public event Action SetMove;

        [SerializeField] private Transform _transform;
        public Transform GetTransform => _transform;
        private void Update()
        {
            SetMove?.Invoke();
        }
    }
}
