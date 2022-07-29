using System;
using UnityEngine;

namespace Assets.Scripts.View
{
    public class WeaponView : MonoBehaviour
    {
        public event Action<Collision2D> CollisionEnter;

        [SerializeField] private Transform _transform;

        public Transform GetTransform => _transform;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            CollisionEnter?.Invoke(collision);
        }
    }
}
