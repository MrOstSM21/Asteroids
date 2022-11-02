using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.View
{

    public class EnemyView : MonoBehaviour
    {
        public event Action<Collision2D> CollisionEnter;
        public event Action GetDamage;

        public Transform GetTransform => _transform;
        public EnemyName EnemyName { get; private set; }

        [SerializeField] private Transform _transform;
        [SerializeField] private SpriteRenderer[] _tailsprites;
        [SerializeField] private Transform[] _explosionSprites;
        [SerializeField] private Transform _body;

        private Sequence _tailAnimation;
        private Tween _bodyRotate;




        private void Start()
        {
            StartTailAnimation();
        }



        private void OnCollisionEnter2D(Collision2D collision)
        {
            CollisionEnter?.Invoke(collision);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public void TakeDamage()
        {
            ExplosionAnimation();
            GetDamage?.Invoke();
            Destroy();
        }
        public void SetEnemyName(EnemyName enemyName) => EnemyName = enemyName;
        private void StartTailAnimation()
        {
            var changeRate = 0.2f;
            _tailAnimation = DOTween.Sequence();
            _tailAnimation.Append(_tailsprites[0].DOFade(100f, changeRate));
            _tailAnimation.Append(_tailsprites[1].DOFade(0f, changeRate));
            _tailAnimation.Append(_tailsprites[2].DOFade(100f, changeRate));
            _tailAnimation.Append(_tailsprites[3].DOFade(0f, changeRate));
            _tailAnimation.Append(_tailsprites[0].DOFade(0f, changeRate));
            _tailAnimation.Append(_tailsprites[1].DOFade(100f, changeRate));
            _tailAnimation.Append(_tailsprites[2].DOFade(0f, changeRate));
            _tailAnimation.Append(_tailsprites[3].DOFade(100f, changeRate));
            _tailAnimation.SetEase(Ease.Linear);
            _tailAnimation.SetLoops(-1);

            _bodyRotate = _body.DORotate(new Vector3(_body.localEulerAngles.x, _body.localEulerAngles.y, _body.localEulerAngles.z-359f), 5f,RotateMode.LocalAxisAdd);
            _bodyRotate.SetEase(Ease.Linear);
            _bodyRotate.SetLoops(-1);
        }
        private void ExplosionAnimation()
        {
            foreach (var item in _explosionSprites)
            {
                var sprite = Instantiate(item, _transform.position, Quaternion.identity);
                Tween tween = sprite.DOScale(new Vector3(0.5f, 0.5f, 0), 1);
                Destroy(sprite);
            }
        }

    }
}