
using Assets.Scripts.View;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class Ship
    {
        private readonly ShipView _shipView;
        private readonly Settings _settings;
        private readonly InputView _inputView;
        private readonly VisibilityHandler _visibilityHandler;
        private readonly GameView _gameView;

        private InertiaHandler _inertiaHandler;
        private IMovement _movement;
        private PoolBullets _poolBullets;

        public Ship(GameView gameView, Settings settings)
        {
            _gameView = gameView;
            _shipView = _gameView.GetShipView;
            _settings = settings;
            _inputView = _gameView.GetInputView;
            _movement = new ForwardMovement(_shipView.GetTransform);
            _inertiaHandler = new InertiaHandler(_shipView.GetTransform, _settings);
            _visibilityHandler = new VisibilityHandler( _shipView.GetTransform);
            _poolBullets = new PoolBullets(gameView.GetContainerPoolBullet, _gameView.GetBulletView, _settings,gameView);
            Subscribe();

        }
      
        private void Move(float movement)
        {
            _visibilityHandler.CheckVisibilityPlayerObjects(this,_gameView.GetMainCamera);
            var direction = _inertiaHandler.MoveInertia(movement);
            _movement.Move(_settings.GetShipForwardAcceleration, direction);

        }
        private void Rotate(float rotation)
        {
            var rotationSpeed = _inertiaHandler.RotateInertia(rotation);
            _shipView.GetTransform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
        private void ShootBullet()
        {
            IWeapon bullet = _poolBullets.GetBullet();
            bullet.Shoot(_gameView.GetWeaponSpawnPoint);

        }

        private void inputView_GetRotation(float rotation) => Rotate(rotation);

        private void inputView_GetMovement(float movement) => Move(movement);
        private void _inputView_GetShootBullet() => ShootBullet();

        private void Subscribe()
        {
            _inputView.GetMovement += inputView_GetMovement;
            _inputView.GetRotation += inputView_GetRotation;
            _inputView.GetShootBullet += _inputView_GetShootBullet;
        }

        private void Unsubscribe()
        {
            _inputView.GetMovement -= inputView_GetMovement;
            _inputView.GetRotation -= inputView_GetRotation;
            _inputView.GetShootBullet += _inputView_GetShootBullet;
        }
    }
}
