using System;
using System.Collections.Generic;
using UnityEngine;

public class InputView : MonoBehaviour
{
    public event Action<float> GetMovement;
    public event Action<float> GetRotation;
    public event Action GetShootBullet;

    private PlayerInput _playerInput;
    private void Awake()
    {
        _playerInput = new PlayerInput();

    }
    private void Update()
    {
        EnterMovement();
        EnterRotation();
        EnterShootBullet();
    }
    
    private void EnterMovement()
    {
        var moveForward = _playerInput.Player.Move.ReadValue<float>();
        GetMovement?.Invoke(moveForward);
    }
    private void EnterRotation()
    {
        var rotation = _playerInput.Player.Rotation.ReadValue<float>();
        GetRotation?.Invoke(rotation);
    }
    private void EnterShootBullet()
    {
        var shootBullet = _playerInput.Player.ShootBullet.ReadValue<float>();
        if (shootBullet!=0)
        {
            GetShootBullet?.Invoke();
        }
    }
    private void OnEnable() => _playerInput.Enable();

    private void OnDisable() => _playerInput.Disable();
}
