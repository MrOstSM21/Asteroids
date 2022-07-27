using Assets.Scripts.View;
using UnityEngine;

namespace Assets.Scripts.Logic
{

    public class Laser : IWeapon
    {
        private const float DISTANCE = 50f;
        private const float STEP_RESTORE = 0.001f;

        public float GetLaserCount => _laserCount;
        public float GetreloadMinValue => _reloadBarMinValue;

        private readonly Animator _animator;
        private readonly WeaponView _weaponView;
        private readonly ShipIndicatorsView _shipIndicatorsView;

        private float _laserCount;
        private float _reloadBarMinValue;

        public Laser(Animator animator)
        {
            _animator = animator;
            _laserCount = 5f;
            _reloadBarMinValue = 0f;
        }

        public void Shoot(Transform weaponSpawnPoint)
        {
            if (_laserCount > 0)
            {
                ActiveLaser(weaponSpawnPoint);
            }
        }

        public void RestoreLaserCounter()
        {
            if (_laserCount < 5)
            {
                _reloadBarMinValue += STEP_RESTORE;
                if (_reloadBarMinValue >= 1)
                {
                    _reloadBarMinValue = 0f;
                    _laserCount++;
                }
            }
        }

        private void ActiveLaser(Transform weaponSpawnPoint)
        {
            _animator.Play("Laser");
            _laserCount--;
            var hits = Physics2D.RaycastAll(weaponSpawnPoint.position, weaponSpawnPoint.up, DISTANCE);
            if (hits != null)
            {
                foreach (var item in hits)
                {
                    if (item.collider.gameObject.TryGetComponent<EnemyView>(out EnemyView enemyView))
                    {
                        enemyView.TakeDamage();
                    }
                }
            }
        }
    }
}
