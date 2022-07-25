﻿using Assets.Scripts.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    class Ufo : IEnemy
    {
        private readonly EnemyView _enemyView;
        private readonly Settings _settings;
        private readonly GameView _gameView;
        private readonly VisibilityHandler _visibilityHandler;

        private IMovement _movement;

        public Ufo(EnemyView enemyView, Settings settings, GameView gameView)
        {
            _enemyView = enemyView;
            _settings = settings;
            _gameView = gameView;
            _visibilityHandler = new VisibilityHandler(_enemyView.GetTransform);
            _movement = new MovementInTarget(_enemyView.GetTransform);
        }

        public void Move() => Subscribe();
        public void LeftTheZone()
        {
            _enemyView.DestroyEnemy();
            Unsubscribe();
        }
      

        private void Subscribe() => _enemyView.SetMove += _enemyView_SetMove;
        private void Unsubscribe() => _enemyView.SetMove -= _enemyView_SetMove;

        private void _enemyView_SetMove()
        {
            _visibilityHandler.CheckVisibilityEnemy(this, _settings.GetEndZoneDistanse);
            var direction = _gameView.GetShipView.GetTransform.position;
            _movement.Move(_settings.GetUfoSpeed, direction);
        }
    }
}
