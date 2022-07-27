using Assets.Scripts.View;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class CollisionHandlerWithWeapon : ICollisionHandler
    {
        public bool CheckCollision(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<WeaponView>())
            {
                return true;
            }
            return false;
        }
    }
}
