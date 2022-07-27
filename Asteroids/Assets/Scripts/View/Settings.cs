using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.View
{
    public class Settings : MonoBehaviour
    {
        [Header("Ship")]

        [SerializeField] private float _shipMaxSpeed;
        [SerializeField] private float _shipForwardAcceleration;
        [SerializeField] private float _shipMaxRotationSpeed;
        [SerializeField] private float _shipRotationAcceleration;

        [Header("Enemy")]

        [SerializeField] private float _asteroidSpeed;
        [SerializeField] private float _ufoSpeed;
        [SerializeField] private float _asteroidPartSpeed;
        [SerializeField] private int _asteroidTimeSpawn;
        [SerializeField] private int _ufoTimeSpawn;
        [SerializeField] private int _asteroidPoint;
        [SerializeField] private int _asteroidPartPoint;
        [SerializeField] private int _ufoPoint;

        private float _endZoneDistance = 11f;

        [Header("Weapon")]

        [SerializeField] private float _bulletSpeed;
        [SerializeField] private int _bulletPoolSize;




        public float GetShipMaxSpeed => _shipMaxSpeed;
        public float GetShipForwardAcceleration => _shipForwardAcceleration;
        public float GetShipMaxRotationSpeed => _shipMaxRotationSpeed;
        public float GetShipRotationAcceleration => _shipRotationAcceleration;

        public float GetAsteroidSpeed => _asteroidSpeed;
        public float GetAsteroidPartSpeed => _asteroidPartSpeed;
        public float GetUfoSpeed => _ufoSpeed;
        public int GetAsteroidTimeSpawn => _asteroidTimeSpawn;
        public int GetUfoTimeSpawn => _ufoTimeSpawn;
        public float GetEndZoneDistanse => _endZoneDistance;

        public float GetBulletSpeed => _bulletSpeed;
        public int GetBulletPoolSize => _bulletPoolSize;

        public Dictionary<EnemyName, int> GetEnemyPoints() => new()
        {
            { EnemyName.Asteroid, _asteroidPoint },
            { EnemyName.AsteroidPart, _asteroidPartPoint },
            { EnemyName.Ufo, _ufoPoint },
        };
    }
}