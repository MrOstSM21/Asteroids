using Assets.Scripts.View;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class CollisionHandlerWithEnemy : ICollisionHandler
    {
        public bool CheckCollision(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<EnemyView>())
            {
                return true;
            }
            return false;
        }
    }
}
