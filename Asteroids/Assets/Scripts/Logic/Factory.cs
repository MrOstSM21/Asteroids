using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.View;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Logic
{
    public class Factory
    {
        private const float DISTANCE_CAMERA_Z = 10f;

        private readonly Settings _settings;
        private readonly Dictionary<EnemyName, EnemyView> _enemyView;
        private readonly GameView _gameView;



        public Factory(Settings settings, Dictionary<EnemyName, EnemyView> enemyView, GameView gameView)
        {
            _enemyView = enemyView;
            _settings = settings;
            _gameView = gameView;

        }
        public IEnemy Create(EnemyName enemyName, Vector3 position)
        {
            IEnemy enemy = null;
            switch (enemyName)
            {
                case EnemyName.Asteroid:
                    var asteroidPrefab = Object.Instantiate(_enemyView[EnemyName.Asteroid], position, Quaternion.identity);
                    enemy = new Asteroid(asteroidPrefab, _settings, GetDirection());
                    break;
                case EnemyName.AsteroidPart:
                    var asteroidPartPrefab = Object.Instantiate(_enemyView[EnemyName.AsteroidPart], position, Quaternion.identity);
                    enemy = new AsteroidPart(asteroidPartPrefab, _settings, GetDirection());
                    break;
                case EnemyName.Ufo:
                    var ufoPrefab = Object.Instantiate(_enemyView[EnemyName.Ufo], position, Quaternion.identity);
                    enemy = new Ufo(ufoPrefab, _settings, _gameView);
                    break;
                default:
                    break;
            }
            return enemy;
        }
        private Vector3 GetDirection()
        {
            var camera = _gameView.GetMainCamera;
            var direction = camera.ViewportToWorldPoint(new Vector3(Random.value, Random.value, DISTANCE_CAMERA_Z));
            return direction;
        }
    }
}
