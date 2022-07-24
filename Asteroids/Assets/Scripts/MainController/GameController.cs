using Assets.Scripts.Logic;
using Assets.Scripts.View;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameView _gameView;
    [SerializeField] private Settings _settings;

    private Ship _ship;
    private EnemyCreateHandler _createHandler;

    private void Start()
    {
        _ship = new Ship(_gameView, _settings);
        _createHandler = new EnemyCreateHandler(_settings, _gameView);
        StartCoroutine(GameTimer());
    }
    
    IEnumerator GameTimer()
    {
        while (true)
        {
            _createHandler.Init();
            yield return new WaitForSeconds(1);
        }
    }
}
