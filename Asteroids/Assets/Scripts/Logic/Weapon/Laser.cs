using Assets.Scripts.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class Laser : IWeapon
    {
        public void Shoot(Transform weaponSpawnPoint)
        {
            throw new NotImplementedException();
        }
    }
}
//transform.up = transformSpawnPoint.up;
//_laserAnimator.Play("LaserAnimation");
//StartLaserSound?.Invoke();

//hits = Physics2D.RaycastAll(startPosition, transform.up, distanse);
//if (hits != null)
//{
//    SetLaserCollisions?.Invoke(hits);
//}