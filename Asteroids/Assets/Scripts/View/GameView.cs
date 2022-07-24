using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyName
{
    Asteroid,
    AsteroidPart,
    Ufo
}
namespace Assets.Scripts.View
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private ShipView _shipView;
        [SerializeField] private EnemyView _asteroid;
        [SerializeField] private EnemyView _asteroidPart;
        [SerializeField] private EnemyView _ufo;
        [SerializeField] private InputView _inputView;
        [SerializeField] private Transform[] _spawnPointsView;
        [SerializeField] private Camera _maincamera;

        public ShipView GetShipView => _shipView;
        public InputView GetInputView => _inputView;
        public Transform[] GetSpawnPointsView => _spawnPointsView;
        public Camera GetMainCamera => _maincamera;

        public Dictionary<EnemyName, EnemyView> GetEnemysView() => new()
        {
            { EnemyName.Asteroid, _asteroid },
            { EnemyName.AsteroidPart, _asteroidPart },
            { EnemyName.Ufo, _ufo },
        };
    }
}
