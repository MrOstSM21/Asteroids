using Assets.Scripts.Logic;
using Assets.Scripts.View;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameView _gameView;
    [SerializeField] private Settings _settings;
    [SerializeField] private SoundView _soundView;

    private Ship _ship;
    private TimedCreationHandler _createHandler;
    private Score _score;
    private UpdateHandler _updateHandler;
    private TimeController _timeController;
    private SaveHandler _saveHandler;
    private TranslateHandler _translateHandler;
    private SoundHandler _soundHandler;

    private void Start()
    {
        _updateHandler = new UpdateHandler();
        _soundHandler = new SoundHandler(_soundView, _updateHandler);
        _ship = new Ship(_gameView, _settings, _updateHandler,_soundHandler);
        _score = new Score(_gameView.GetScoreView);
        _saveHandler = new SaveHandler();
        _createHandler = new TimedCreationHandler(_settings, _gameView, _score, _updateHandler,_soundHandler);
        _timeController = new TimeController(_updateHandler);
        _translateHandler = new TranslateHandler(_gameView.GetTranslateView, _updateHandler);
        
        SetStartParameters();
    }

    private void Update()
    {
        _updateHandler.Init();
    }


    public void ReloadGame()
    {
        _saveHandler.Save(_score.CheckBestScore(), _translateHandler.Lang, _soundHandler.GetMusicState, AudioListener.pause);
        _updateHandler.FinishGame();
        _ship.ShipDestroy -= ShipDestroy;
        SceneManager.LoadScene(0);
    }
    public void AddRewardHealth()
    {
        if (!_ship.ActiveBonusHealth)
        {
            _ship.AddBonusHealth();
            _gameView.GetEndPanel.SetActive(false);
            _timeController.TimeStart();
        }
    }
    public void StartGame()
    {
        _gameView.GetStartPanel.SetActive(false);
        _timeController.TimeStart();
        _updateHandler.StartActions();
    }

    private void ShipDestroy()
    {
        _gameView.GetEndPanel.SetActive(true);
        _gameView.GetEndScoreView.SetScore(_score.GetScore);
        _gameView.GetBestScoreView.SetScore(_score.GetBestScore);
        _timeController.TimeStop();
    }
    private void SetStartParameters()
    {
        StartCoroutine(GameTimer());
        _score.SetBestScore(_saveHandler.Load()._score);
        SetTranslate(_saveHandler.Load()._lang);
        _soundHandler.SetStartParameters(_saveHandler.Load()._music, _saveHandler.Load()._sound);
        _ship.ShipDestroy += ShipDestroy;
        _timeController.TimeStop();

    }
    private void SetTranslate(int lang)
    {
        if (lang == 0)
            _translateHandler.SetTranslateRu();
        else if (lang == 1)
            _translateHandler.SetTranslateEn();
        else
            _translateHandler.SetTranslateRu();
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
