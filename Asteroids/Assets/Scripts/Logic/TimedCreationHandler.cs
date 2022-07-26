﻿using UnityEngine;
using Assets.Scripts.View;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Logic
{
    public class TimedCreationHandler
    {
        private const float DISTANCE_CAMERA_Z = 10f;

        private readonly Settings _settings;
        private readonly GameView _gameView;
        private readonly Transform[] _spawnPoints;
        private readonly Score _score;
        private readonly UpdateHandler _updateHandler;
        private readonly SoundHandler _soundHandler;

        private int _time;

        public TimedCreationHandler(Settings settings, GameView gameView, Score score, UpdateHandler updateHandler,SoundHandler soundHandler)
        {
            _gameView = gameView;
            _soundHandler = soundHandler;
            _spawnPoints = _gameView.GetSpawnPointsView;
            _score = score;
            _settings = settings;
            _updateHandler = updateHandler;
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
            if (_time > 0 && _time % _settings.GetLifeItemTimeSpawn == 0)
            {
                CreateEnemy(EnemyName.LifeItem, GetSpawnPoint(_spawnPoints));
            }
            if (_time > 0 && _time % _settings.GetPointsItemTimeSpawn == 0)
            {
                CreateEnemy(EnemyName.PointsItem, GetSpawnPoint(_spawnPoints));
            }
            if (_time > 0 && _time % _settings.GetSpeedItemTimeSpawn == 0)
            {
                CreateEnemy(EnemyName.SpeedItem, GetSpawnPoint(_spawnPoints));
            }
        }

        private void CreateEnemy(EnemyName enemyName, Vector3 position)
        {
            var factory = new Factory(_settings, _gameView, _updateHandler,_soundHandler);
            factory.Create(enemyName, position, GetDirection(), _score);
        }
        private Vector3 GetSpawnPoint(Transform[] positions)
        {
            var indexPosition = Random.Range(0, positions.Length - 1);
            var position = positions[indexPosition].position;

            return position;
        }
        private Vector3 GetDirection()
        {
            var camera = _gameView.GetMainCamera;
            var direction = camera.ViewportToWorldPoint(new Vector3(Random.value, Random.value, DISTANCE_CAMERA_Z));

            return direction;
        }
    }
}
