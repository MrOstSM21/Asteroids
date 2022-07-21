using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Logic
{
    public interface IMovement
    {
        public void Move(float speed, Vector3 dirrection);
    }
}

