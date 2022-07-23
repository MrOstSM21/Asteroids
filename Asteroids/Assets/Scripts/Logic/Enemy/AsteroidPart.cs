using Assets.Scripts.View;
using System;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class AsteroidPart:IEnemy
    {
        private readonly EnemyView _enemyView;
        private readonly Settings _settings;
        private readonly Vector3 _direction;

        private IMovement _movement;
        public AsteroidPart(EnemyView enemyView, Settings settings, Vector3 direction)
        {
            _enemyView = enemyView;
            _settings = settings;
            _direction = direction;
            _movement = new ForwardMovement(_enemyView.GetTransform);
        }

        public void Move() => Subscribe();

        private void Subscribe() => _enemyView.SetMove += _enemyView_SetMove;
        private void Unsubscribe() => _enemyView.SetMove -= _enemyView_SetMove;

        private void _enemyView_SetMove()
        {
            _movement.Move(_settings.GetAsteroidSpeed, _direction);
        }
    }
}
