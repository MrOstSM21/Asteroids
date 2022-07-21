using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class Ship
    {
        private ShipView _shipView;
        private IMovement _movement;
        private Settings _settings;
        private InputView _inputView;
        private InertiaHandler _inertiaHandler;

        public Ship(ShipView shipView, Settings settings, InputView inputView)
        {
            _shipView = shipView;
            _settings = settings;
            _inputView = inputView;
            _movement = new ForwardMovement(_shipView.GetTransform);
            _inertiaHandler = new InertiaHandler(_shipView.GetTransform, _settings.GetShipMaxSpeed, _settings.GetMaxRotationSpeed,
                _settings.GetRotationAcceleration);
            Subscribe();

        }
        private void Move(float movement)
        {
            var speed = _inertiaHandler.MoveInertia(movement);
            _movement.Move(_settings.GetShipForwardAcceleration, speed);
        }
        private void Rotate(float rotation)
        {
            var rotationSpeed = _inertiaHandler.RotateInertia(rotation);
            _shipView.GetTransform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
        private void Subscribe()
        {
            _inputView.GetMovement += inputView_GetMovement;
            _inputView.GetRotation += inputView_GetRotation;
        }

        private void inputView_GetRotation(float rotation)
        {
            Rotate(rotation);
        }

        private void inputView_GetMovement(float movement)
        {
            Move(movement);
            
        }
    }
}
