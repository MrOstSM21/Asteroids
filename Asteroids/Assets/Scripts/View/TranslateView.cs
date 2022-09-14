using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.View
{
    public enum Translate
    {
        NewGame,
        Help,
        Position,
        Rotation,
        Speed,
        LaserAmmo,
        Reloading,
        YourScore,
        BestScore,
        TryAgain,
        RewardHealth
    }
    public class TranslateView : MonoBehaviour
    {
        public event Action SetRU;
        public event Action SetEN;

        [SerializeField] private TextMeshProUGUI _newGame;
        [SerializeField] private TextMeshProUGUI _help;
        [SerializeField] private TextMeshProUGUI _position;
        [SerializeField] private TextMeshProUGUI _rotation;
        [SerializeField] private TextMeshProUGUI _speed;
        [SerializeField] private TextMeshProUGUI _laserAmmo;
        [SerializeField] private TextMeshProUGUI _reloading;
        [SerializeField] private TextMeshProUGUI _tryAgain;
        [SerializeField] private TextMeshProUGUI _rewardPoints;
        [SerializeField] private ScoreView _endScore;
        [SerializeField] private ScoreView _bestScore;

        public void SetTranslate(Dictionary<Translate, string> translate)
        {
            _newGame.text = translate[Translate.NewGame];
            _help.text = translate[Translate.Help];
            _position.text = translate[Translate.Position];
            _rotation.text = translate[Translate.Rotation];
            _speed.text = translate[Translate.Speed];
            _laserAmmo.text = translate[Translate.LaserAmmo];
            _reloading.text = translate[Translate.Reloading];
            _tryAgain.text = translate[Translate.TryAgain];
            _rewardPoints.text = translate[Translate.RewardHealth];
            _endScore.SetScoreName(translate[Translate.YourScore]);
            _bestScore.SetScoreName(translate[Translate.BestScore]);
        }
        public void SetRu() => SetRU?.Invoke();
        public void SetEn() => SetEN?.Invoke();
    }
}
