using Assets.Scripts.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Logic
{
    public class PoolBullets
    {
        private readonly Transform _parentContainer;
        private readonly BulletView _bulletView;
        private readonly Settings _settings;
        private readonly int _poolSize;
        private readonly GameView _gameView;

        private List<Bullet> _bullets;

       

        public PoolBullets(Transform parentContainer,BulletView bulletView,Settings settings,GameView gameView)
        {
            _parentContainer = parentContainer;
            _settings = settings;
            _bulletView = bulletView;
            _gameView = gameView;
            _poolSize = _settings.GetBulletPoolSize;
            Init();
        }
        private void AddBullet()
        {
            var tempBullet = Object.Instantiate(_bulletView);
            var bullet = new Bullet(tempBullet,_settings,_gameView.GetMainCamera);
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
        
    }
}
