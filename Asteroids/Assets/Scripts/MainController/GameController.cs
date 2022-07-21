using Assets.Scripts.Logic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameView _gameView;
    [SerializeField] private Settings _settings;
    
    private Ship _ship;

    private void Start()
    {
        _ship = new Ship(_gameView.GetShipView, _settings,_gameView.GetInputView);
    }
   
}
