using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.View
{
    public class ShipView : MonoBehaviour
    {
        [SerializeField] Transform _transform;
        public Transform GetTransform => _transform;

    }
}