using Assets.Scripts.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class Bullet : IWeapon
    {
        public Transform GetTransform => _bulletView.GetTransform;

        private readonly BulletView _bulletView;
        private readonly Settings _settings;
        private readonly VisibilityHandler _visibilityHandler;
        private readonly Camera _camera;

        //private ShipView _shipView;
        private Vector3 _direction;
        private IMovement _movement;

        public Bullet(BulletView bulletView, Settings settings,Camera camera)
        {
            _camera = camera;
            _bulletView = bulletView;
            _settings = settings;
            _visibilityHandler = new VisibilityHandler( _bulletView.GetTransform);

            _movement = new ForwardMovement(_bulletView.GetTransform);
        }

        public void Shoot(Transform weaponSpawnPoint)
        {
            //_shipView = shipView;
            SetStartParameter(weaponSpawnPoint);
            Subscribe();
        }
        public void LeftTheZone() => ReturnInPool();
        private void ReturnInPool()
        {
            _bulletView.GetTransform.gameObject.SetActive(false);
            Unsubscribe();
        }

        private void SetStartParameter(Transform weaponSpawnPoint)
        {
            _direction = weaponSpawnPoint.transform.up;
            var positionY = weaponSpawnPoint.position.y;
            _bulletView.GetTransform.position = new Vector3(weaponSpawnPoint.position.x, positionY, 0f);
            _bulletView.GetTransform.rotation = weaponSpawnPoint.rotation;
        }
        private void Subscribe() => _bulletView.SetMove += _bulletView_SetMove;
        private void Unsubscribe() => _bulletView.SetMove -= _bulletView_SetMove;
        private void _bulletView_SetMove()
        {
            _visibilityHandler.CheckVisibilityPlayerObjects(this,_camera);
            _movement.Move(_settings.GetBulletSpeed, _direction);
        }

    }
}
