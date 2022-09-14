using Assets.Scripts.View;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Logic
{
    class Asteroid : Enemy
    {
        private readonly GameView _gameView;
        private readonly ICollisionHandler _collisionHandler;

        public Asteroid(EnemyView enemyView, Settings settings, Vector3 direction, GameView gameView, Score score,UpdateHandler updateHandler,SoundHandler soundHandler)
            : base(settings, score, updateHandler,soundHandler)
        {
            _enemy = this;
            _explosiveSound = SoundName.Explosion;
            _speed = _settings.GetAsteroidSpeed;
            _enemyView = enemyView;
            _gameView = gameView;
            _direction = (direction - _enemyView.GetTransform.position).normalized;
            _visibilityHandler = new VisibilityHandler(_enemyView.GetTransform);
            _movement = new ForwardMovement(_enemyView.GetTransform);
            _collisionHandler = new CollisionHandlerWithWeapon();
            _enemyView.SetEnemyName(EnemyName.Asteroid);
            _enemyPoints = _settings.GetEnemyPoints()[EnemyName.Asteroid];
            Subscribe();
        }


        private void CollisionEnter(Collision2D collision)
        {
            var weapon = _collisionHandler.CheckCollision(collision);
            if (weapon)
            {
                CreateParts();
                PlayDestroySound();
                _enemyView.TakeDamage();

                Unsubscribe();
            }
        }

        public override void Subscribe()
        {
            _updateHandler.Update += Move;
            _enemyView.CollisionEnter += CollisionEnter;
            _enemyView.GetDamage += GetDamage;
        }

        public override void Unsubscribe()
        {
            _enemyView.CollisionEnter -= CollisionEnter;
            _updateHandler.Update -= Move;
            _enemyView.GetDamage -= GetDamage;
        }
        private void CreateParts()
        {
            for (int count = 0; count < 3; count++)
            {
                var direction = new Vector3(Random.Range(-360f, 360f), Random.Range(-360f, 360f), 0f);
                var factory = new Factory(_settings, _gameView, _updateHandler,_soundHandler);
                factory.Create(EnemyName.AsteroidPart, _enemyView.GetTransform.position, direction, _score);
            }
        }

    }
}
