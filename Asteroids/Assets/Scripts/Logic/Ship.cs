
using Assets.Scripts.View;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class Ship
    {
        private readonly ShipView _shipView;
        private readonly Settings _settings;
        private readonly InputView _inputView;

        private InertiaHandler _inertiaHandler;
        private IMovement _movement;

        public Ship(ShipView shipView, Settings settings, InputView inputView)
        {
            _shipView = shipView;
            _settings = settings;
            _inputView = inputView;
            _movement = new ForwardMovement(_shipView.GetTransform);
            _inertiaHandler = new InertiaHandler(_shipView.GetTransform, _settings);
            Subscribe();

        }
        private void Move(float movement)
        {
            var direction = _inertiaHandler.MoveInertia(movement);
            _movement.Move(_settings.GetShipForwardAcceleration, direction);
        }
        private void Rotate(float rotation)
        {
            var rotationSpeed = _inertiaHandler.RotateInertia(rotation);
            _shipView.GetTransform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }

        private void inputView_GetRotation(float rotation) => Rotate(rotation);

        private void inputView_GetMovement(float movement) => Move(movement);

        private void Subscribe()
        {
            _inputView.GetMovement += inputView_GetMovement;
            _inputView.GetRotation += inputView_GetRotation;
        }
        private void Unsubscribe()
        {
            _inputView.GetMovement -= inputView_GetMovement;
            _inputView.GetRotation -= inputView_GetRotation;
        }
    }
}
