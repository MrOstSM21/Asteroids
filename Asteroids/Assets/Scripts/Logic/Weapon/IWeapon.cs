using Assets.Scripts.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public interface IWeapon
    {
        public void Shoot(Transform weaponSpawnPoint);
    }
}
