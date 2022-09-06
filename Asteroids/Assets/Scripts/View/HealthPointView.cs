
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.View
{
    public class HealthPointView : MonoBehaviour
    {
        private int _maxHp;

        [SerializeField] private Sprite[] _sprites;
        [SerializeField] private Image _image;

        private void Awake()
        {
            _maxHp = _sprites.Length;
        }
        public void SetSprite(int index)
        {
            if (index > 0)
            {
                _image.sprite = _sprites[index - 1];
            }
        }

        public int GetMaxHp() => _maxHp;
    }
}
