﻿
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.View;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Logic
{
    public class Factory
    {
        private readonly Settings _settings;
        private readonly Dictionary<EnemyName, EnemyView> _enemyView;
        private readonly GameView _gameView;
        private readonly UpdateHandler _updateHandler;

        public Factory(Settings settings, GameView gameView,UpdateHandler updateHandler)
        {
            _settings = settings;
            _gameView = gameView;
            _updateHandler = updateHandler;
            _enemyView = _gameView.GetEnemysView();
        }

        public IEnemy Create(EnemyName enemyName, Vector3 position, Vector3 direction, Score score)
        {
            IEnemy enemy = null;
            switch (enemyName)
            {
                case EnemyName.Asteroid:
                    var asteroidPrefab = Object.Instantiate(_enemyView[EnemyName.Asteroid], position, Quaternion.identity);
                    enemy = new Asteroid(asteroidPrefab, _settings, direction, _gameView, score, _updateHandler);
                    break;
                case EnemyName.AsteroidPart:
                    var asteroidPartPrefab = Object.Instantiate(_enemyView[EnemyName.AsteroidPart], position, Quaternion.identity);
                    enemy = new AsteroidPart(asteroidPartPrefab, _settings, direction, score,_updateHandler);
                    break;
                case EnemyName.Ufo:
                    var ufoPrefab = Object.Instantiate(_enemyView[EnemyName.Ufo], position, Quaternion.identity);
                    enemy = new Ufo(ufoPrefab, _settings, _gameView, score,_updateHandler);
                    break;
                default:
                    break;
            }
            return enemy;
        }
    }
}
