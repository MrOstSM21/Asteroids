using System;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    class ForwardMovement : IMovement
    {
        private Transform _transform;

        public ForwardMovement(Transform transform)
        {
            _transform = transform;
        }

        public void Move(float speed, Vector3 dirrection)
        {
            _transform.position += dirrection * speed * Time.deltaTime;
        }
    }
}
