using System.Collections.Generic;
using UnityEngine;

public enum EnemyName
{
    Asteroid,
    AsteroidPart,
    Ufo,
    LifeItem,
    PointsItem,
    SpeedItem
}
namespace Assets.Scripts.View
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private ShipView _shipView;
        [SerializeField] private EnemyView _asteroid;
        [SerializeField] private EnemyView _asteroidPart;
        [SerializeField] private EnemyView _ufo;
        [SerializeField] private EnemyView _lifeItem;
        [SerializeField] private EnemyView _pointsItem;
        [SerializeField] private EnemyView _speedItem;
        [SerializeField] private InputView _inputView;
        [SerializeField] private Transform[] _spawnPointsView;
        [SerializeField] private Camera _maincamera;
        [SerializeField] private Transform _containerPoolBullet;
        [SerializeField] private WeaponView _weaponView;
        [SerializeField] private Transform _weaponSpawnPoint;
        [SerializeField] private ShipIndicatorsView _shipIndicatorsView;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private ScoreView _endScoreView;
        [SerializeField] private ScoreView _bestScoreView;
        [SerializeField] private GameObject _endPanel;
        [SerializeField] private GameObject _startPanel;
        [SerializeField] private HealthPointView _shipHealthPointView;
        [SerializeField] private TranslateView _translateView;
        

        public ShipView GetShipView => _shipView;
        public InputView GetInputView => _inputView;
        public Transform[] GetSpawnPointsView => _spawnPointsView;
        public Camera GetMainCamera => _maincamera;
        public Transform GetContainerPoolBullet => _containerPoolBullet;
        public WeaponView GetWeaponView => _weaponView;
        public Transform GetWeaponSpawnPoint => _weaponSpawnPoint;
        public ShipIndicatorsView GetShipIndicators => _shipIndicatorsView;
        public ScoreView GetScoreView => _scoreView;
        public ScoreView GetEndScoreView => _endScoreView;
        public ScoreView GetBestScoreView => _bestScoreView;
        public GameObject GetEndPanel => _endPanel;
        public GameObject GetStartPanel => _startPanel;
        public HealthPointView GetShipHealthPointView => _shipHealthPointView;
        public TranslateView GetTranslateView => _translateView;
        


        public Dictionary<EnemyName, EnemyView> GetEnemysView() => new()
        {
            { EnemyName.Asteroid, _asteroid },
            { EnemyName.AsteroidPart, _asteroidPart },
            { EnemyName.Ufo, _ufo },
            { EnemyName.LifeItem, _lifeItem },
            { EnemyName.PointsItem, _pointsItem },
            { EnemyName.SpeedItem,_speedItem}
        };
    }
}
