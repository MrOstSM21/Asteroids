using Assets.Scripts.Logic;
using Assets.Scripts.View;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameView _gameView;
    [SerializeField] private Settings _settings;

    private Ship _ship;
    private TimedCreationHandler _createHandler;
    private Score _score;
    private UpdateHandler _updateHandler;

    private void Start()
    {
        _updateHandler = new UpdateHandler();
        _ship = new Ship(_gameView, _settings,_updateHandler);
        _score = new Score(_gameView.GetScoreView);
        _createHandler = new TimedCreationHandler(_settings, _gameView, _score,_updateHandler);
        StartCoroutine(GameTimer());
        _ship.EndGame += EndGame;
        Time.timeScale = 1;
    }

    private void Update()
    {
        _updateHandler.Init();
    }
    IEnumerator GameTimer()
    {
        while (true)
        {
            _createHandler.Init();
            yield return new WaitForSeconds(1);
        }
    }

    public void ReloadGame() => SceneManager.LoadScene(0);

    private void EndGame()
    {
        _gameView.GetEndScoreView.SetScore(_score.GetScore);
        _gameView.GetEndPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
