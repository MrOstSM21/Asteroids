using Assets.Scripts.View;
using UnityEngine;

namespace Assets.Scripts.Logic
{

    class InertiaHandler
    {
        private const float STEP_SPEED = 1f;

        private readonly Transform _transform;
        private readonly float _maxSpeed;
        private readonly float _maxRotationSpeed;
        private readonly float _rotationAcceleration;

        private float _rotationSpeed;
        private Vector3 _moveSpeed;

        public InertiaHandler(Transform transform, Settings shipsettings)
        {
            _transform = transform;
            _maxSpeed = shipsettings.GetShipMaxSpeed;
            _maxRotationSpeed = shipsettings.GetShipMaxRotationSpeed;
            _rotationAcceleration = shipsettings.GetShipRotationAcceleration;
        }

        public Vector3 MoveInertia(float input)
        {
            _moveSpeed *= (STEP_SPEED - Time.deltaTime);
            _moveSpeed = Vector3.ClampMagnitude(_moveSpeed, _maxSpeed);
            var acceleration = input * _transform.up;
            _moveSpeed += acceleration * Time.deltaTime;

            return _moveSpeed;
        }
        public float RotateInertia(float input)
        {
            _rotationSpeed *= (STEP_SPEED - Time.deltaTime * _rotationAcceleration);
            _rotationSpeed = Mathf.Clamp(_rotationSpeed, -_maxRotationSpeed, _maxRotationSpeed);
            _rotationSpeed += -input;

            return _rotationSpeed;
        }
    }
}
