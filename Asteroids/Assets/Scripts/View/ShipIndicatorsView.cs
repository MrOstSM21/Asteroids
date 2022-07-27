using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.View
{
    public class ShipIndicatorsView : MonoBehaviour
    {
        [SerializeField] private Text _position;
        [SerializeField] private Text _rotation;
        [SerializeField] private Text _speed;
        [SerializeField] private Text _laserAmmo;
        [SerializeField] private Image _progressBar;

        private void Start()
        {
            _progressBar.fillAmount = 0f;
        }

        public void UISetShipMovement(Vector3 position, Vector3 rotation, float speed)
        {
            var binaryPositionX = (int)(position.x * 100) / 100f;
            var binaryPositionY = (int)(position.y * 100) / 100f;
            _position.text = $"X: {binaryPositionX}, Y: {binaryPositionY} ";

            var angle = (int)(rotation.z * 100) / 100f;
            _rotation.text = $"Angle: {angle}";

            var shipSpeed = (int)(speed * 10f);
            _speed.text = $"{shipSpeed}";
        }
        public void UISetLaserAmmo(float ammo, float reload)
        {
            _laserAmmo.text = $"{ammo}";
            _progressBar.fillAmount = reload;
        }
    }
}
