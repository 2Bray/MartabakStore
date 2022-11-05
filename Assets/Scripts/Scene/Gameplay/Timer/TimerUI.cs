using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TimerUI : MonoBehaviour
{
    private TextMeshProUGUI _timeUI;

    public void Setup()
    {
        _timeUI = GetComponent<TextMeshProUGUI>();
    }

    public void ShowTime(float time)
    {
        int min = (int)time / 60;
        int sec = (int)time - min * 60;

        string s_min = min < 10 ? "0" + min : "" + min;
        string s_sec = sec < 10 ? "0" + sec : "" + sec;

        _timeUI.text = s_min + " : " + s_sec;
    }
}
