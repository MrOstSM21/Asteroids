﻿using Assets.Scripts.View;
using System;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class Ship
    {
        public event Action ShipDestroy;

        public bool ActiveBonusHealth { get; private set; }

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
        private readonly UpdateHandler _updateHandler;
        private readonly HealthPointsHandler _healthPointsHandler;
        private readonly SoundHandler _soundHandler;

        private float _speed;


        public Ship(GameView gameView, Settings settings, UpdateHandler updateHandler,SoundHandler soundHandler)
        {
            ActiveBonusHealth = false;
            _soundHandler = soundHandler;
            _updateHandler = updateHandler;
            _gameView = gameView;
            _shipView = _gameView.GetShipView;
            _shipIndicatorsView = _gameView.GetShipIndicators;
            _settings = settings;
            _inputView = _gameView.GetInputView;
            _movement = new ForwardMovement(_shipView.GetTransform);
            _inertiaHandler = new InertiaHandler(_shipView.GetTransform, _settings);
            _visibilityHandler = new VisibilityHandler(_shipView.GetTransform);
            _poolBullets = new PoolBullets(gameView.GetContainerPoolBullet, _gameView.GetWeaponView, _settings, gameView, _updateHandler);
            _laser = new Laser(_shipView.GetAnimator);
            _collisionHandler = new CollisionHandlerWithEnemy();
            _healthPointsHandler = new HealthPointsHandler(_settings.GetShipStartHp, _gameView.GetShipHealthPointView);
            Subscribe();
        }

        public void AddBonusHealth()
        {
            ActiveBonusHealth = true;
            _healthPointsHandler.AddHP();
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
            if (TimeController.GameIsStart)
            {
                IWeapon bullet = _poolBullets.GetBullet();
                if (bullet!=null)
                {
                    bullet.Shoot(_gameView.GetWeaponSpawnPoint);
                    _soundHandler.PlaySound(SoundName.Fire);
                }
               
            }
        }
        private void CollisionEnter(Collision2D collision)
        {
            var enemy = _collisionHandler.CheckCollision(collision);
            if (enemy)
            {
                var point = _healthPointsHandler.DecreaseHP();
                if (point <= 0)
                {
                    ShipDestroy?.Invoke();

                }
            }
        }
        private void FinishGame()
        {
            _poolBullets.Unsubscribe();
            Unsubscribe();
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

        private void GetShootLaser()
        {
            _laser.Shoot(_gameView.GetWeaponSpawnPoint);
            _soundHandler.PlaySound(SoundName.Laser);
        }

        private void GetEnemyCollision(Collision2D collision)
        {
            var enemy = collision.gameObject.GetComponent<EnemyView>().EnemyName;
            if (enemy == EnemyName.LifeItem)
            {
                _healthPointsHandler.AddHP();
            }
        }
       
        private void Subscribe()
        {
            _inputView.GetMovement += GetMovement;
            _inputView.GetRotation += GetRotation;
            _inputView.GetShootBullet += GetShootBullet;
            _inputView.GetShootLaser += GetShootLaser;
            _updateHandler.Update += ShipUpdate;
            _updateHandler.EndGame += FinishGame;
            _shipView.CollisionEnter += CollisionEnter;
            _poolBullets.GetEnemy += GetEnemyCollision;
        }

        private void Unsubscribe()
        {
            _inputView.GetMovement -= GetMovement;
            _inputView.GetRotation -= GetRotation;
            _inputView.GetShootBullet -= GetShootBullet;
            _inputView.GetShootLaser -= GetShootLaser;
            _updateHandler.Update -= ShipUpdate;
            _updateHandler.EndGame -= FinishGame;
            _shipView.CollisionEnter -= CollisionEnter;
            _poolBullets.GetEnemy -= GetEnemyCollision;
        }
    }
}
