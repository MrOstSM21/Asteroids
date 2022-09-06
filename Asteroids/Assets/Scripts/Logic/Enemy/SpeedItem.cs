using Assets.Scripts.View;
using UnityEngine;

namespace Assets.Scripts.Logic
{

    public class SpeedItem : Enemy
    {
        private readonly GameView _gameView;
        private readonly ICollisionHandler _collisionHandler;

        private int _hit = 1;

        public SpeedItem(EnemyView enemyView, Settings settings, GameView gameView, Score score, UpdateHandler updateHandler)
            : base(settings, score, updateHandler)
        {
            _enemy = this;
            _speed = _settings.GetSpeedItemSpeed;
            _enemyView = enemyView;
            _gameView = gameView;
            _visibilityHandler = new VisibilityHandler(_enemyView.GetTransform);
            _movement = new MovementInTarget(_enemyView.GetTransform);
            _collisionHandler = new CollisionHandlerWithWeapon();
            _enemyView.SetEnemyName(EnemyName.SpeedItem);
            _enemyPoints = _settings.GetEnemyPoints()[EnemyName.SpeedItem];
            Subscribe();
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

        private void CollisionEnter(Collision2D collision)
        {
            var weapon = _collisionHandler.CheckCollision(collision);
            if (weapon)
            {
                CheckHit();
            }
        }
        private void CheckHit()
        {
            if (_hit > 0)
            {
                _speed += _settings.GetSpeedItemSpeedOffset;
                _hit--;
            }
            else
            {
                _enemyView.TakeDamage();
                Unsubscribe();
            }
        }
        private void Update()
        {
            _direction = _gameView.GetShipView.GetTransform.position;
            Move();
        }
    }
}
