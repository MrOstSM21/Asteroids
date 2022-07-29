using System;
using UnityEngine;

namespace Assets.Scripts.View
{
    public class ShipView : MonoBehaviour
    {
        public event Action<Collision2D> CollisionEnter;

        [SerializeField] private Transform _transform;
        [SerializeField] private Animator _animator;

        public Transform GetTransform => _transform;
        public Animator GetAnimator => _animator;


        private void OnCollisionEnter2D(Collision2D collision)
        {
            CollisionEnter?.Invoke(collision);
        }
    }
}