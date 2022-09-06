using Assets.Scripts.View;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class PointsItem : Enemy
    {
        private readonly ICollisionHandler _collisionHandler;
        public PointsItem(EnemyView enemyView, Settings settings, Vector3 direction, Score score, UpdateHandler updateHandler)
            : base(settings, score, updateHandler)
        {
            _enemy = this;
            _speed = _settings.GetPointsItemSpeed;
            _enemyView = enemyView;
            _movement = new ForwardMovement(_enemyView.GetTransform);
            _visibilityHandler = new VisibilityHandler(_enemyView.GetTransform);
            _collisionHandler = new CollisionHandlerWithWeapon();
            _direction = (direction - _enemyView.GetTransform.position).normalized;
            _enemyView.SetEnemyName(EnemyName.PointsItem);
            _enemyPoints = _settings.GetEnemyPoints()[EnemyName.PointsItem];
            Subscribe();
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

        private void CollisionEnter(Collision2D collision)
        {
            var weapon = _collisionHandler.CheckCollision(collision);
            if (weapon)
            {
                _enemyView.TakeDamage();
                Unsubscribe();
            }
        }
    }
}
