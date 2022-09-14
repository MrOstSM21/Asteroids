using Assets.Scripts.View;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    class Ufo : Enemy
    {
        private readonly GameView _gameView;
        private readonly ICollisionHandler _collisionHandler;

        public Ufo(EnemyView enemyView, Settings settings, GameView gameView, Score score, UpdateHandler updateHandler,SoundHandler soundHandler)
            : base(settings, score, updateHandler, soundHandler)
        {
            _enemy = this;
            _explosiveSound = SoundName.Explosion;
            _speed = _settings.GetUfoSpeed;
            _enemyView = enemyView;
            _gameView = gameView;
            _visibilityHandler = new VisibilityHandler(_enemyView.GetTransform);
            _movement = new MovementInTarget(_enemyView.GetTransform);
            _collisionHandler = new CollisionHandlerWithWeapon();
            _enemyView.SetEnemyName(EnemyName.Ufo);
            _enemyPoints = _settings.GetEnemyPoints()[EnemyName.Ufo];
            Subscribe();
        }

        private void CollisionEnter(Collision2D collision)
        {
            var weapon = _collisionHandler.CheckCollision(collision);
            if (weapon)
            {
                PlayDestroySound();
                _enemyView.TakeDamage();
                Unsubscribe();
            }
        }
        public override void Subscribe()
        {
            _updateHandler.Update += Update;
            _enemyView.CollisionEnter += CollisionEnter;
            _enemyView.GetDamage += GetDamage;
        }

        public override void Unsubscribe()
        {
            _enemyView.CollisionEnter -= CollisionEnter;
            _updateHandler.Update -= Update;
            _enemyView.GetDamage -= GetDamage;
        }
        private void Update()
        {
            _direction = _gameView.GetShipView.GetTransform.position;
            Move();
        }
    }
}
