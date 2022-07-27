using Assets.Scripts.View;
using System;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class Ship
    {
        public event Action EndGame;

        private readonly ShipView _shipView;
        private readonly Settings _settings;
        private readonly InputView _inputView;
        private readonly VisibilityHandler _visibilityHandler;
        private readonly GameView _gameView;
        private readonly ShipIndicatorsView _shipIndicatorsView;
        private readonly InertiaHandler _inertiaHandler;
        private readonly ICollisionHandler _collisionHandler;
        private readonly IMovement _movement;
        private readonly PoolBullets _poolBullets;
        private readonly IWeapon _laser;

        private float _speed;

        public Ship(GameView gameView, Settings settings)
        {
            _gameView = gameView;
            _shipView = _gameView.GetShipView;
            _shipIndicatorsView = _gameView.GetShipIndicators;
            _settings = settings;
            _inputView = _gameView.GetInputView;
            _movement = new ForwardMovement(_shipView.GetTransform);
            _inertiaHandler = new InertiaHandler(_shipView.GetTransform, _settings);
            _visibilityHandler = new VisibilityHandler(_shipView.GetTransform);
            _poolBullets = new PoolBullets(gameView.GetContainerPoolBullet, _gameView.GetWeaponView, _settings, gameView);
            _laser = new Laser(_shipView.GetAnimator);
            _collisionHandler = new CollisionHandlerWithEnemy();
            Subscribe();
        }

        private void Move(float movement)
        {
            _visibilityHandler.CheckVisibilityPlayerObjects(this, _gameView.GetMainCamera);
            var direction = _inertiaHandler.MoveInertia(movement);
            _speed = direction.magnitude;
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
            bullet?.Shoot(_gameView.GetWeaponSpawnPoint);
        }
        private void CollisionEnter(Collision2D collision)
        {
            var enemy = _collisionHandler.CheckCollision(collision);
            if (enemy)
            {
                EndGame?.Invoke();
                Unsubscribe();
            }
        }
        private void ShipUpdate()
        {
            _shipIndicatorsView.UISetShipMovement(_shipView.transform.position, _shipView.transform.rotation.eulerAngles, _speed);
            var laser = (Laser)_laser;
            laser.RestoreLaserCounter();
            _shipIndicatorsView.UISetLaserAmmo(laser.GetLaserCount, laser.GetreloadMinValue);
        }

        private void GetRotation(float rotation) => Rotate(rotation);

        private void GetMovement(float movement) => Move(movement);

        private void GetShootBullet() => ShootBullet();

        private void GetShootLaser() => _laser.Shoot(_gameView.GetWeaponSpawnPoint);

        private void Subscribe()
        {
            _inputView.GetMovement += GetMovement;
            _inputView.GetRotation += GetRotation;
            _inputView.GetShootBullet += GetShootBullet;
            _inputView.GetShootLaser += GetShootLaser;
            _shipView.ShipUpdate += ShipUpdate;
            _shipView.CollisionEnter += CollisionEnter;
        }

        private void Unsubscribe()
        {
            _inputView.GetMovement -= GetMovement;
            _inputView.GetRotation -= GetRotation;
            _inputView.GetShootBullet -= GetShootBullet;
            _inputView.GetShootLaser -= GetShootLaser;
            _shipView.ShipUpdate -= ShipUpdate;
            _shipView.CollisionEnter -= CollisionEnter;
        }
    }
}
