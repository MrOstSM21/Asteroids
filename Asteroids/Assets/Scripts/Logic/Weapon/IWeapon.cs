using UnityEngine;

namespace Assets.Scripts.Logic
{
    public interface IWeapon
    {
        public void Shoot(Transform weaponSpawnPoint);
    }
}
