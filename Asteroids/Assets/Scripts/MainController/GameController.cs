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
    private TimeController _timeController;
    private SaveHandler _saveHandler;

    private void Start()
    {
        _updateHandler = new UpdateHandler();
        _ship = new Ship(_gameView, _settings, _updateHandler);
        _score = new Score(_gameView.GetScoreView);
        _saveHandler = new SaveHandler();
        _createHandler = new TimedCreationHandler(_settings, _gameView, _score, _updateHandler);
        _timeController = new TimeController(_updateHandler);
        SetStartParameters();
    }

    private void Update()
    {
        _updateHandler.Init();
    }


    public void ReloadGame() => SceneManager.LoadScene(0);
    public void StartGame()
    {
        _gameView.GetStartPanel.SetActive(false);
        _timeController.TimeStart();
        _updateHandler.StartActions();
    }

    private void EndGame()
    {
        _gameView.GetEndPanel.SetActive(true);
        _gameView.GetEndScoreView.SetScore(_score.GetScore);
        _saveHandler.Save(_score.CheckBestScore());
        _gameView.GetBestScoreView.SetScore(_score.GetBestScore);

        _timeController.TimeStop();
    }
    private void SetStartParameters()
    {
        StartCoroutine(GameTimer());
        _score.SetBestScore(_saveHandler.Load()._score);
        _ship.EndGame += EndGame;
        _timeController.TimeStop();

    }
    IEnumerator GameTimer()
    {
        while (true)
        {
            _createHandler.Init();
            yield return new WaitForSeconds(1);
        }
    }

}
