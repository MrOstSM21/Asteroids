using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.View;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Logic
{
    public class Factory
    {
        private readonly Settings _settings;
        private readonly Dictionary<EnemyName, EnemyView> _enemyView;
        


        public Factory(Settings settings, Dictionary<EnemyName, EnemyView> enemyView)
        {
            _enemyView = enemyView;
            _settings = settings;

        }
        public IEnemy Create(EnemyName enemyName, Vector3 direction, Vector3 position)
        {
            IEnemy enemy = null;
            switch (enemyName)
            {
                case EnemyName.Asteroid:
                    var asteroidPrefab = Object.Instantiate(_enemyView[EnemyName.Asteroid], position, Quaternion.identity);
                    enemy = new Asteroid(asteroidPrefab, _settings,direction);
                    break;
                case EnemyName.AsteroidPart:
                    var asteroidPartPrefab = Object.Instantiate(_enemyView[EnemyName.AsteroidPart], position, Quaternion.identity);
                    enemy = new AsteroidPart(asteroidPartPrefab, _settings,direction);
                    break;
                case EnemyName.Ufo:
                    var ufoPrefab = Object.Instantiate(_enemyView[EnemyName.Ufo], position, Quaternion.identity);
                    enemy = new Ufo(ufoPrefab, _settings, direction);
                    break;
                default:
                    break;
            }
            return enemy;
        }
    }
}
