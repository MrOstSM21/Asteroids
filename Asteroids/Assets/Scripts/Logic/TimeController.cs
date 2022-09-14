using Assets.Scripts.Logic;
using System;
using UnityEngine;

public class TimeController
{
    public static bool GameIsStart { get; private set; }

    private readonly UpdateHandler _updateHandler;
    public TimeController(UpdateHandler updateHandler)
    {
        GameIsStart = false;
        _updateHandler = updateHandler;
        _updateHandler.StartGameActions +=StartGameActions;
    }
    public void TimeStart() => Time.timeScale = 1;
    public void TimeStop() => Time.timeScale = 0;
    private void StartGameActions()
    {
        GameIsStart = true;
    }
    private void Subscribe()
    {
        _updateHandler.StartGameActions += StartGameActions;
        _updateHandler.EndGame += Unsubscribe;
    }

    private void Unsubscribe()
    {
        _updateHandler.StartGameActions -= StartGameActions;
        _updateHandler.EndGame -= Unsubscribe;
    }
}
