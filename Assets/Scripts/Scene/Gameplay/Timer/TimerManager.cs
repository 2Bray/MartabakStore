using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public Action TimeOver;
    public Action<float> Alarm;

    private static TimerManager _instance;
    public static TimerManager Instance { get => _instance; }

    [SerializeField] private TimerUI _timerUI;

    private float _gameDuration;

    public float CurrentTime { get => _time; }
    private float _time;

    private bool _isGameStop;

    public void Setup()
    {

        _timerUI.Setup();
        _instance = this;

        _time = 0;
        _gameDuration = 0;

        _isGameStop = true;
    }

    private void Update()
    {
        if (_isGameStop)
            return;

        if (_time >= _gameDuration)
        {
            _isGameStop = true;
            TimeOver?.Invoke();
            return;
        }

        _time += Time.deltaTime;

        _timerUI.ShowTime(_gameDuration - _time);
        Alarm?.Invoke(_time);
    }

    public void SetGameDuration(float value)
    {
        _gameDuration = value;
        _isGameStop = false;
    }

    public void StopTime() => _isGameStop = true;
}
