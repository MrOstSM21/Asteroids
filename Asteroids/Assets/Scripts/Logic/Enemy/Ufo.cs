using Assets.Scripts.View;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    class Ufo : IEnemy
    {
        private readonly EnemyView _enemyView;
        private readonly Settings _settings;
        private readonly GameView _gameView;
        private readonly VisibilityHandler _visibilityHandler;
        private readonly Score _score;
        private readonly IMovement _movement;
        private readonly ICollisionHandler _collisionHandler;
        private readonly Dictionary<EnemyName, int> _enemyPoints;
        private readonly UpdateHandler _updateHandler;

        public Ufo(EnemyView enemyView, Settings settings, GameView gameView, Score score, UpdateHandler updateHandler)
        {
            _enemyView = enemyView;
            _settings = settings;
            _gameView = gameView;
            _score = score;
            _updateHandler = updateHandler;
            _enemyPoints = _settings.GetEnemyPoints();
            _visibilityHandler = new VisibilityHandler(_enemyView.GetTransform);
            _movement = new MovementInTarget(_enemyView.GetTransform);
            _collisionHandler = new CollisionHandlerWithWeapon();
            Subscribe();
        }

        public void Move() => _updateHandler.Update += SetMove;

        public void LeftTheZone()
        {
            Unsubscribe();
            _enemyView.Destroy();
        }

        private void SetMove()
        {
            _visibilityHandler.CheckVisibilityEnemy(this, _settings.GetEndZoneDistanse);
            var direction = _gameView.GetShipView.GetTransform.position;
            _movement.Move(_settings.GetUfoSpeed, direction);
        }

        private void CollisionEnter(Collision2D collision)
        {
            var weapon = _collisionHandler.CheckCollision(collision);
            if (weapon)
            {
                _enemyView.TakeDamage();
                Unsubscribe();
            }
        }

        private void GetDamage()
        {
            _score.AddPoint(_enemyPoints[EnemyName.Ufo]);
            Unsubscribe();
        }
        private void Subscribe()
        {
            _enemyView.CollisionEnter += CollisionEnter;
            _enemyView.GetDamage += GetDamage;
        }

        private void Unsubscribe()
        {
            _enemyView.CollisionEnter -= CollisionEnter;
            _updateHandler.Update -= SetMove;
            _enemyView.GetDamage -= GetDamage;
        }
    }
}
