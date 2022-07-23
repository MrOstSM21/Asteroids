using System;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.View;

namespace Assets.Scripts.Logic
{
    public class EnemyCreateHandler
    {
        private readonly Settings _settings;
        private readonly ShipView _shipView;
        private readonly Dictionary<EnemyName, EnemyView> _enemyView;

        private float _time;
        private Vector3 _possition;
        private Vector3 _direction;

        public EnemyCreateHandler(Settings settings, ShipView shipView, Dictionary<EnemyName, EnemyView> enemyView)
        {
            _enemyView = enemyView;
            _settings = settings;
            _shipView = shipView;

        }

        public void Init()
        {
            _time++;
            if ((int)_time > 0 && (int)_time % _settings.GetAsteroidTimeSpawn == 0)
            {
                CreateEnemy(EnemyName.Asteroid, _direction, _possition);
            }
            if ((int)_time > 0 && (int)_time % _settings.GetUfoTimeSpawn == 0)
            {
                CreateEnemy(EnemyName.Ufo, _shipView.GetTransform.position, _possition);
            }

        }
        private void CreateEnemy(EnemyName enemyName, Vector3 direction, Vector3 position)
        {
            var factory = new Factory(_settings, _enemyView);
            IEnemy enemy = factory.Create(enemyName, direction, position);
            enemy.Move();
        }

    }

}
