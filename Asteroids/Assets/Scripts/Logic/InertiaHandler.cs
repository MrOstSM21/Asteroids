using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Logic
{

    class InertiaHandler
    {
        private const float STEP_SPEED = 1f;

        private Transform _transform;
        private Vector3 _moveSpeed;
        private float _maxSpeed;
        private float _rotationSpeed;
        private float _maxRotationSpeed;
        private float _rotationAcceleration;

        public InertiaHandler(Transform transform, float maxSpeed, float maxRotationSpeed, float rotationAcceleration)
        {
            _transform = transform;
            _maxSpeed = maxSpeed;
            _maxRotationSpeed = maxRotationSpeed;
            _rotationAcceleration = rotationAcceleration;

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
            _rotationSpeed *= (STEP_SPEED - Time.deltaTime);
            _rotationSpeed = Mathf.Clamp(_rotationSpeed, -_maxRotationSpeed, _maxRotationSpeed);
            float turnAcceleration = input * _rotationAcceleration;
            _rotationSpeed += -turnAcceleration * Time.deltaTime;
            return _rotationSpeed;
        }

    }
}
