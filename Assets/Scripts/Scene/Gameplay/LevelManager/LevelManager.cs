using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private LevelDatabase _levelDatabase;
    private PartSetting _currentLevel;

    public int MaxCustomerOrder { get => _currentLevel.MaxCustomerOrder; }
    public int TargetCustomer { get => _currentLevel.CustomerTargetCount; }
    public bool IsStopWhenTarget { get => _currentLevel.IsStopWhenTarget; }

    private int _stageNumber;
    private int _partNumber;

    public void Setup(LevelDatabase levelDatabase, GameFlow gameFlow)
    {
        _levelDatabase = levelDatabase;

        _stageNumber = MoveSceneRequest.Instance.Stage;
        _partNumber = PlayerData.Instance.StageData.GetStageByNumber(_stageNumber).Part;

        SetLevel(_stageNumber, _partNumber);

        TimerManager.Instance.SetGameDuration(_currentLevel.GameDuration);

        gameFlow.OnStagePassed += StagePassed;
    }

    private void SetLevel(int stage, int part) =>
        _currentLevel = _levelDatabase.GetStageByNumber(stage).GetPartByNumber(part);

    private void StagePassed() =>
        PlayerData.Instance.StageData.StagePassed(_stageNumber);
}