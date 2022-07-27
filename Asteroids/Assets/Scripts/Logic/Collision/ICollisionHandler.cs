using UnityEngine;

namespace Assets.Scripts.Logic
{
    public interface ICollisionHandler
    {
        public bool CheckCollision(Collision2D collision);
    }
}
