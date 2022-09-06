using Assets.Scripts.View;
using System;
using UnityEngine;

namespace Assets.Scripts.Logic
{

    public class Bullet : IWeapon
    {
        public event Action<Collision2D> GetEnemy;
        public Transform GetTransform => _weaponView.GetTransform;

        private readonly WeaponView _weaponView;
        private readonly Settings _settings;
        private readonly VisibilityHandler _visibilityHandler;
        private readonly Camera _camera;
        private readonly IMovement _movement;
        private readonly ICollisionHandler _collisionHandler;
        private readonly UpdateHandler _updateHandler;

        private Vector3 _direction;

        public Bullet(WeaponView weaponView, Settings settings, Camera camera, UpdateHandler updateHandler)
        {
            _camera = camera;
            _weaponView = weaponView;
            _settings = settings;
            _updateHandler = updateHandler;
            _visibilityHandler = new VisibilityHandler(_weaponView.GetTransform);
            _collisionHandler = new CollisionHandlerWithEnemy();
            _movement = new ForwardMovement(_weaponView.GetTransform);
        }

        public void Shoot(Transform weaponSpawnPoint)
        {
            SetStartParameter(weaponSpawnPoint);
            Subscribe();
        }
        public void LeftTheZone() => ReturnInPool();

        private void ReturnInPool()
        {
            _weaponView.GetTransform.gameObject.SetActive(false);
            Unsubscribe();
        }

        private void SetStartParameter(Transform weaponSpawnPoint)
        {
            _direction = weaponSpawnPoint.transform.up;
            var positionY = weaponSpawnPoint.position.y;
            _weaponView.GetTransform.position = new Vector3(weaponSpawnPoint.position.x, positionY, 0f);
            _weaponView.GetTransform.rotation = weaponSpawnPoint.rotation;
        }
        private void Update()
        {
            _visibilityHandler.CheckVisibilityPlayerObjects(this, _camera);
            _movement.Move(_settings.GetBulletSpeed, _direction);
        }

        private void CollisionEnter(Collision2D collision)
        {
            var enemy = _collisionHandler.CheckCollision(collision);
            if (enemy)
            {
                GetEnemy?.Invoke(collision);
                ReturnInPool();
            }
        }

        private void Subscribe()
        {
            _weaponView.CollisionEnter += CollisionEnter;
            _updateHandler.Update += Update;
        }

        private void Unsubscribe()
        {
            _updateHandler.Update -= Update;
            _weaponView.CollisionEnter -= CollisionEnter;
        }
    }
}
