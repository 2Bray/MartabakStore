using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanSliderUI : MonoBehaviour
{
    private Slider _timerSlider;
    [SerializeField] private Image _timerSliderProgress;

    public void Setup(Vector2 pos)
    {
        _timerSlider = GetComponent<Slider>();
        transform.position = Camera.main.WorldToScreenPoint(pos);
    }

    public void SetTimerSliderProperty(float MaxValue, Color color)
    {
        _timerSlider.maxValue = MaxValue;
        _timerSliderProgress.color = color;
    }

    public void SetValue(float value) => _timerSlider.value = value;

    public void SetActiveSlider(bool isActive) => gameObject.SetActive(isActive);
}
