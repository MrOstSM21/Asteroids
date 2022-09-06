using Assets.Scripts.View;
using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Logic
{
    public class PoolBullets
    {
        public event Action<Collision2D> GetEnemy;

        private const float BULLET_DELAY = 2f;
        private const float STEP_DELAY = 0.1f;

        private readonly Transform _parentContainer;
        private readonly WeaponView _weaponView;
        private readonly Settings _settings;
        private readonly int _poolSize;
        private readonly GameView _gameView;
        private readonly UpdateHandler _updateHandler;

        private List<Bullet> _bullets;
        private float _fireDelay;

        public PoolBullets(Transform parentContainer, WeaponView weaponView, Settings settings, GameView gameView, UpdateHandler updateHandler)
        {
            _parentContainer = parentContainer;
            _settings = settings;
            _weaponView = weaponView;
            _gameView = gameView;
            _updateHandler = updateHandler;
            _poolSize = _settings.GetBulletPoolSize;
            _fireDelay = BULLET_DELAY;
            Init();
        }
        private void AddBullet()
        {
            var tempBullet = Object.Instantiate(_weaponView);
            var bullet = new Bullet(tempBullet, _settings, _gameView.GetMainCamera, _updateHandler);
            Subscribe(bullet);
            bullet.GetTransform.SetParent(_parentContainer);
            _bullets.Add(bullet);
            bullet.GetTransform.gameObject.SetActive(false);
        }

        private void Init()
        {
            _bullets = new List<Bullet>();
            for (int i = 0; i < _poolSize; i++)
            {
                AddBullet();
            }
        }

        public Bullet GetBullet()
        {
            _fireDelay -= STEP_DELAY;
            if (_fireDelay <= 0)
            {
                _fireDelay = BULLET_DELAY;
                for (int i = 0; i < _bullets.Count; i++)
                {
                    if (!_bullets[i].GetTransform.gameObject.activeInHierarchy)
                    {
                        _bullets[i].GetTransform.gameObject.SetActive(true);
                        return _bullets[i];
                    }
                }
                AddBullet();

                _bullets[_bullets.Count - 1].GetTransform.gameObject.SetActive(true);

                return _bullets[_bullets.Count - 1];
            }

            return null;
        }

        public void Unsubscribe()
        {
            foreach (var bullet in _bullets)
            {
                bullet.GetEnemy -= GetDamagedEnemy;
            }
        }

        private void GetDamagedEnemy(Collision2D collision) => GetEnemy?.Invoke(collision);

        private void Subscribe(Bullet bullet) => bullet.GetEnemy += GetDamagedEnemy;
    }
}
