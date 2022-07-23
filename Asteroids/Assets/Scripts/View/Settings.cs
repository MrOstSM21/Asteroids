using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.View
{
    public class Settings : MonoBehaviour
    {

        [SerializeField] private float _shipMaxSpeed;
        [SerializeField] private float _shipForwardAcceleration;
        [SerializeField] private float _shipMaxRotationSpeed;
        [SerializeField] private float _shipRotationAcceleration;

        [SerializeField] private float _asteroidSpeed;
        [SerializeField] private float _ufoSpeed;
        [SerializeField] private int _asteroidTimeSpawn;
        [SerializeField] private int _ufoTimeSpawn;
       


        public float GetShipMaxSpeed => _shipMaxSpeed;
        public float GetShipForwardAcceleration => _shipForwardAcceleration;
        public float GetShipMaxRotationSpeed => _shipMaxRotationSpeed;
        public float GetShipRotationAcceleration => _shipRotationAcceleration;

        public float GetAsteroidSpeed => _asteroidSpeed;
        public float GetUfoSpeed => _ufoSpeed;
        public int GetAsteroidTimeSpawn => _asteroidTimeSpawn;
        public int GetUfoTimeSpawn => _ufoTimeSpawn;
    }
}