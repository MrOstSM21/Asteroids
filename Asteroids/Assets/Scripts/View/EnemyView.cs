using System;
using UnityEngine;

namespace Assets.Scripts.View
{
   
    public class EnemyView : MonoBehaviour
    {
        public event Action SetMove;
        public event Action<Collision2D> CollisionEnter;
        public event Action GetDamage;

        public Transform GetTransform => _transform;

        [SerializeField] private Transform _transform;

        private int _points;
                
        private void Update()
        {
            SetMove?.Invoke();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            CollisionEnter?.Invoke(collision);
        }
        public void Init(int points) => _points = points;
       
        public void Destroy() => Destroy(gameObject);

        public void TakeDamage()
        {
            GetDamage?.Invoke();
            Destroy();
        }
    }
}