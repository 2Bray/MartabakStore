using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : MonoBehaviour, IClickable, IPan
{
    [SerializeField] private PanSliderUI _panSliderTimer;
    [SerializeField] private Transform _panSliderPosition;

    private bool _isFree;

    [SerializeField] private float _timeCook;
    [SerializeField] private float _timeRangeToOverCook;
    private float _timeDone;

    private MartabakController _currentMartabak;
    private bool _isDone;

    private PlateController _plateController;
    private TimerManager _timerManager;

    public void Setup(PlateController plateController)
    {
        _timerManager = TimerManager.Instance;
        _plateController = plateController;

        _panSliderTimer.Setup(_panSliderPosition.position);

        _isFree = true;
    }

    public void OnClicked()
    {
        if (!_isDone) 
            return;

        IPlate plate = _plateController.RequestPlate();
        if (plate != null)
        {
            _isDone = false;
            _timerManager.Alarm -= StartCook;
            
            _panSliderTimer.SetActiveSlider(false);

            plate.ServeToPlate(_currentMartabak);
            _isFree = true;
        }
    }

    public void Cooking(MartabakController martabak)
    {
        _isFree = false;

        _currentMartabak = martabak;
        _currentMartabak.SetPosition(transform.position);

        _panSliderTimer.SetActiveSlider(true);
        _panSliderTimer.SetTimerSliderProperty(_timeCook, Color.green);

        _timeDone = _timerManager.CurrentTime + _timeCook;
        _timerManager.Alarm += StartCook;
    }

    private void StartCook(float time)
    {
        SetTimerSliderTime(time);

        if (time >= _timeDone && !_isDone)
        {
            _isDone = true;
            _currentMartabak.MartabakDone();
            _panSliderTimer.SetTimerSliderProperty(_timeRangeToOverCook, Color.red);
        }
        else if (time >= _timeDone + _timeRangeToOverCook)
        {
            _timerManager.Alarm -= StartCook;

            _currentMartabak.OverCook();
            _panSliderTimer.SetActiveSlider(false);
        }
    }

    private void SetTimerSliderTime(float value)
    {
        if (!_isDone)
            _panSliderTimer.SetValue((value - _timeDone) + _timeCook);
        else
            _panSliderTimer.SetValue((_timeRangeToOverCook + _timeDone) - value);
    }

    public bool IsFree() => _isFree;
}
