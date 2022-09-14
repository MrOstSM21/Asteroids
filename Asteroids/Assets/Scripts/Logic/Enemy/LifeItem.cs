using Assets.Scripts.View;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class LifeItem : Enemy
    {
        private readonly ICollisionHandler _collisionHandler;
        public LifeItem(EnemyView enemyView, Settings settings, Vector3 direction, Score score, UpdateHandler updateHandler,SoundHandler soundHandler)
            : base(settings, score, updateHandler,soundHandler)
        {
            _enemy = this;
            _explosiveSound = SoundName.Explosion;
            _speed = _settings.GetLifeItemSpeed;
            _enemyView = enemyView;
            _movement = new ForwardMovement(_enemyView.GetTransform);
            _visibilityHandler = new VisibilityHandler(_enemyView.GetTransform);
            _collisionHandler = new CollisionHandlerWithWeapon();
            _direction = (direction - _enemyView.GetTransform.position).normalized;
            _enemyView.SetEnemyName(EnemyName.LifeItem);
            _enemyPoints = _settings.GetEnemyPoints()[EnemyName.LifeItem];
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
                PlayDestroySound();
                _enemyView.TakeDamage();
                Unsubscribe();
            }
        }
    }
}

