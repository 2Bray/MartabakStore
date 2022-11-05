using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameOverController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _result;
    [SerializeField] private TextMeshProUGUI _target;
    [SerializeField] private TextMeshProUGUI _customerServe;

    [SerializeField] private Button _back;
    [SerializeField] private Button _continue;
    [SerializeField] private Button _retry;

    public void Setup()
    {
        _back.onClick.AddListener(() => MoveSceneRequest.Instance.MoveTo("MainMenu"));
        _continue.onClick.AddListener(() => MoveSceneRequest.Instance.MoveTo("MainMenu"));
        _retry.onClick.AddListener(() => MoveSceneRequest.Instance.MoveTo("Gameplay"));
    }

    public void SetResult(bool isWin, int target, int customerServeCount)
    {
        _target.text = "Target:\t" + target;
        _customerServe.text = "Customer Serve:\t" + customerServeCount;

        if (isWin)
            SetPlayerWin();
        else
            SetPlayerLost();

        gameObject.SetActive(true);
    }

    private void SetPlayerWin()
    {
        _result.color = Color.green;
        _result.text = "Stage Passed";
        _continue.gameObject.SetActive(true);
    }

    private void SetPlayerLost()
    {
        _result.color = Color.red;
        _result.text = "You Lost";
        _retry.gameObject.SetActive(true);
    }
}
