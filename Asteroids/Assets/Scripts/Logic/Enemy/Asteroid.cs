using Assets.Scripts.View;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Logic
{
    class Asteroid : IEnemy
    {
        private readonly EnemyView _enemyView;
        private readonly Settings _settings;
        private readonly Vector3 _direction;
        private readonly VisibilityHandler _visibilityHandler;
        private readonly GameView _gameView;
        private readonly Score _score;
        private readonly IMovement _movement;
        private readonly ICollisionHandler _collisionHandler;
        private readonly Dictionary<EnemyName, int> _enemyPoints;

        public Asteroid(EnemyView enemyView, Settings settings, Vector3 direction, GameView gameView, Score score)
        {
            _enemyView = enemyView;
            _gameView = gameView;
            _settings = settings;
            _score = score;
            _direction = direction - _enemyView.GetTransform.position;
            _enemyPoints = _settings.GetEnemyPoints();
            _visibilityHandler = new VisibilityHandler(_enemyView.GetTransform);
            _movement = new ForwardMovement(_enemyView.GetTransform);
            _collisionHandler = new CollisionHandlerWithWeapon();
            Subscribe();
        }

        public void Move() => _enemyView.SetMove += SetMove;
        public void LeftTheZone()
        {
            _enemyView.Destroy();
            Unsubscribe();
        }
        private void SetMove()
        {
            _visibilityHandler.CheckVisibilityEnemy(this, _settings.GetEndZoneDistanse);
            _movement.Move(_settings.GetAsteroidSpeed, _direction.normalized);
        }

        private void CollisionEnter(Collision2D collision)
        {
            var weapon = _collisionHandler.CheckCollision(collision);
            if (weapon)
            {
                CreateParts();
                _enemyView.TakeDamage();
                Unsubscribe();
            }
        }

        private void CreateParts()
        {
            for (int count = 0; count < 3; count++)
            {
                var direction = new Vector3(Random.Range(-360f, 360f), Random.Range(-360f, 360f), 0f);
                var factory = new Factory(_settings, _gameView);
                var part = factory.Create(EnemyName.AsteroidPart, _enemyView.GetTransform.position, direction, _score);
                part?.Move();
            }
        }

        private void GetDamage() => _score.AddPoint(_enemyPoints[EnemyName.Asteroid]);

        private void Subscribe()
        {
            _enemyView.CollisionEnter += CollisionEnter;
            _enemyView.GetDamage += GetDamage;
        }

        private void Unsubscribe()
        {
            _enemyView.CollisionEnter -= CollisionEnter;
            _enemyView.SetMove -= SetMove;
            _enemyView.GetDamage -= GetDamage;
        }
    }
}
