using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour, ICustomerGetServeSubcriber
{
    public Action OnGameOver;
    public Action OnStagePassed;

    private bool _isStopWhenTarget; 

    private int _customerTargetCount;
    private int _customerCount;

    private TimerManager _timerManager;
    private GameOverController _gameOverController;


    public void Setup(
        GameOverController gameOverController, 
        int customerTargetCount, 
        bool isStopWhenTarget
        )
    {
        _timerManager = TimerManager.Instance;

        _timerManager.TimeOver += GameOver;

        _customerTargetCount = customerTargetCount;
        _isStopWhenTarget = isStopWhenTarget;

        _gameOverController = gameOverController;

        _customerCount = 0;
    }

    public void SetLevelSetting(bool isStopWhenTarget, int customerTargetCount)
    {
        _isStopWhenTarget = isStopWhenTarget;
        _customerTargetCount = customerTargetCount;
    }

    public void OnCustomerGetServe()
    {
        _customerCount++;

        if (_isStopWhenTarget && _customerCount >= _customerTargetCount)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        OnGameOver();

        _timerManager.StopTime();
        _timerManager.TimeOver -= GameOver;

        bool isWin = _customerCount >= _customerTargetCount;

        if (isWin)
            OnStagePassed?.Invoke();

        _gameOverController.SetResult(isWin, _customerTargetCount, _customerCount);
    }
}
