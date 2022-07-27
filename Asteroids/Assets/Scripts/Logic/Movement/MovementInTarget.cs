using UnityEngine;

namespace Assets.Scripts.Logic
{
    class MovementInTarget : IMovement
    {
        private readonly Transform _transformObject;
        public MovementInTarget(Transform transform)
        {
            _transformObject = transform;
        }

        public void Move(float speed, Vector3 targetPosition) =>
                    _transformObject.position = Vector3.MoveTowards(_transformObject.position, targetPosition, speed * Time.deltaTime);
    }
}
