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
        [SerializeField] private int _shipStartHp;

        [Header("Enemy")]

        [SerializeField] private float _asteroidSpeed;
        [SerializeField] private float _ufoSpeed;
        [SerializeField] private float _asteroidPartSpeed;
        [SerializeField] private float _lifeItemSpeed;
        [SerializeField] private float _pointsItemSpeed;
        [SerializeField] private float _speedItemSpeed;
        [SerializeField] private float _speedItemSpeedOffset;
        [SerializeField] private int _asteroidTimeSpawn;
        [SerializeField] private int _ufoTimeSpawn;
        [SerializeField] private int _lifeItemTimeSpawn;
        [SerializeField] private int _pointsItemTimeSpawn;
        [SerializeField] private int _speedItemTimeSpawn;
        [SerializeField] private int _asteroidPoint;
        [SerializeField] private int _asteroidPartPoint;
        [SerializeField] private int _ufoPoint;
        [SerializeField] private int _lifeItemPoint;
        [SerializeField] private int _pointsItemPoint;
        [SerializeField] private int _speedItemPoint;

        private float _endZoneDistance = 11f;

        [Header("Weapon")]

        [SerializeField] private float _bulletSpeed;
        [SerializeField] private int _bulletPoolSize;




        public float GetShipMaxSpeed => _shipMaxSpeed;
        public float GetShipForwardAcceleration => _shipForwardAcceleration;
        public float GetShipMaxRotationSpeed => _shipMaxRotationSpeed;
        public float GetShipRotationAcceleration => _shipRotationAcceleration;
        public int GetShipStartHp => _shipStartHp;

        public float GetAsteroidSpeed => _asteroidSpeed;
        public float GetAsteroidPartSpeed => _asteroidPartSpeed;
        public float GetUfoSpeed => _ufoSpeed;
        public float GetLifeItemSpeed => _lifeItemSpeed;
        public float GetPointsItemSpeed => _pointsItemSpeed;
        public float GetSpeedItemSpeed => _speedItemSpeed;
        public float GetSpeedItemSpeedOffset => _speedItemSpeedOffset;
        public int GetAsteroidTimeSpawn => _asteroidTimeSpawn;
        public int GetUfoTimeSpawn => _ufoTimeSpawn;
        public int GetLifeItemTimeSpawn => _lifeItemTimeSpawn;
        public int GetPointsItemTimeSpawn => _pointsItemTimeSpawn;
        public int GetSpeedItemTimeSpawn => _speedItemTimeSpawn;
        public float GetEndZoneDistanse => _endZoneDistance;

        public float GetBulletSpeed => _bulletSpeed;
        public int GetBulletPoolSize => _bulletPoolSize;

        public Dictionary<EnemyName, int> GetEnemyPoints() => new()
        {
            { EnemyName.Asteroid, _asteroidPoint },
            { EnemyName.AsteroidPart, _asteroidPartPoint },
            { EnemyName.Ufo, _ufoPoint },
            { EnemyName.LifeItem,_lifeItemPoint},
            { EnemyName.PointsItem,_pointsItemPoint},
            { EnemyName.SpeedItem,_speedItemPoint}
        };
    }
}