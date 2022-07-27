using UnityEngine;

namespace Assets.Scripts.Logic
{
    class ForwardMovement : IMovement
    {
        private readonly Transform _transformObject;

        public ForwardMovement(Transform transform)
        {
            _transformObject = transform;
        }

        public void Move(float speed, Vector3 dirrection) => _transformObject.position += dirrection * speed * Time.deltaTime;
    }
}
