using UnityEngine;

namespace Assets.Scripts.Logic
{

    public class VisibilityHandler
    {
        private readonly Transform _transform;

        public VisibilityHandler(Transform transform)
        {
            _transform = transform;
        }

        public void CheckVisibilityPlayerObjects<T>(T entity, Camera camera)
        {
            var position = _transform.position;
            Vector2 screenPosition = camera.WorldToViewportPoint(_transform.position);

            if (screenPosition.x < 0 || screenPosition.x > 1)
            {
                if (entity is Ship)
                {
                    position.x = -position.x;
                    _transform.position = position;
                }

                if (entity is Bullet bullet)
                {
                    bullet.LeftTheZone();
                }
            }
            if (screenPosition.y < 0 || screenPosition.y > 1)
            {
                if (entity is Ship)
                {
                    position.y = -position.y;
                    _transform.position = position;
                }

                if (entity is Bullet bullet)
                {
                    bullet.LeftTheZone();
                }
            }
        }
        public void CheckVisibilityEnemy(Enemy enemy, float endZoneDistance)
        {
            var position = _transform.position;
            if (position.x < -endZoneDistance || position.x > endZoneDistance || position.y < -endZoneDistance || position.y > endZoneDistance)
            {
                enemy.LeftTheZone();
            }
        }
    }
}
