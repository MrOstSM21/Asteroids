using System;
using UnityEngine;

namespace Assets.Scripts.View
{

    public class EnemyView : MonoBehaviour
    {
        public event Action<Collision2D> CollisionEnter;
        public event Action GetDamage;

        public Transform GetTransform => _transform;

        [SerializeField] private Transform _transform;


        private void OnCollisionEnter2D(Collision2D collision)
        {
            CollisionEnter?.Invoke(collision);
        }

        public void Destroy() => Destroy(gameObject);

        public void TakeDamage()
        {
            GetDamage?.Invoke();
            Destroy();
        }
    }
}