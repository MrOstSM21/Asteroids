using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    class VisibilityHandler
    {
        private readonly Camera _camera;
        private Transform _transform;

        public VisibilityHandler(Camera camera, Transform transform)
        {
            _camera = camera;
            _transform = transform;
        }

        public void CheckVisibility()
        {
            var position = _transform.position;
            Vector2 screenPosition = _camera.WorldToViewportPoint(_transform.position);

            if (screenPosition.x < 0 || screenPosition.x > 1)
            {

                position.x = -position.x;
                _transform.position = position;
                

            }
            if (screenPosition.y < 0 || screenPosition.y > 1)
            {
                position.y = -position.y;
                _transform.position = position;

            }

            
        }
    }
}
