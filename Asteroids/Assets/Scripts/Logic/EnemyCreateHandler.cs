using System;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.View;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Logic
{
    public class EnemyCreateHandler
    {


        private readonly Settings _settings;
        private readonly Dictionary<EnemyName, EnemyView> _enemyView;
        private readonly GameView _gameView;
        private readonly Transform[] _spawnPoints;

        private int _time;



        public EnemyCreateHandler(Settings settings, GameView gameView)
        {
            _gameView = gameView;
            _spawnPoints = _gameView.GetSpawnPointsView;
            _enemyView = _gameView.GetEnemysView();
            _settings = settings;

        }

        public void Init()
        {
            _time++;
            if (_time > 0 && _time % _settings.GetAsteroidTimeSpawn == 0)
            {
                CreateEnemy(EnemyName.Asteroid, GetSpawnPoint(_spawnPoints));
            }
            if (_time > 0 && _time % _settings.GetUfoTimeSpawn == 0)
            {
                CreateEnemy(EnemyName.Ufo, GetSpawnPoint(_spawnPoints));
            }

        }
        private void CreateEnemy(EnemyName enemyName, Vector3 position)
        {
            var factory = new Factory(_settings, _enemyView, _gameView);
            IEnemy enemy = factory.Create(enemyName, position);
            enemy?.Move();
        }
        private Vector3 GetSpawnPoint(Transform[] positions)
        {
            var indexPosition = Random.Range(0, positions.Length - 1);
            var position = positions[indexPosition].position;
            return position;
        }
    }

}
