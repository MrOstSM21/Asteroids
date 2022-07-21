using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{

    [SerializeField] private float _shipMaxSpeed;
    [SerializeField] private float _shipForwardAcceleration;
    [SerializeField] private float _maxRotationSpeed;
    [SerializeField] private float _rotationAcceleration;

    public float GetShipMaxSpeed => _shipMaxSpeed;
    public float GetShipForwardAcceleration => _shipForwardAcceleration;
    public float GetMaxRotationSpeed => _maxRotationSpeed;
    public float GetRotationAcceleration => _rotationAcceleration;
}
