using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData
{
    private AllStage _allStage;

    private void Save()
    {
        PlayerPrefs.SetString("StageData", JsonUtility.ToJson(_allStage));
    }

    public StageData Load()
    {
        string json = PlayerPrefs.GetString("StageData");

        if (string.IsNullOrEmpty(json))
        {
            _allStage = new AllStage();
            _allStage.Stages = new Stage[0];

            CreateNewStage(1).SetUnlock();
            CreateNewStage(2);
        }
        else
            _allStage = JsonUtility.FromJson<AllStage>(json);

        return this;
    }

    public Stage GetStageByNumber(int stageNumber)
    {
        foreach (Stage stage in _allStage.Stages)
        {
            if (stage.StageNumber == stageNumber)
            {
                return stage;
            }
        }

        return CreateNewStage(stageNumber);
    }

    private Stage CreateNewStage(int stageNumber)
    {
        Stage[] temp = new Stage[_allStage.Stages.Length + 1];

        int index = 0;
        foreach (Stage stage in _allStage.Stages)
        {
            temp[index] = stage;
            index++;
        }

        Stage newStage = new Stage(stageNumber);

        temp[index] = newStage;
        _allStage.Stages = temp;

        return newStage;
    }

    public void StagePassed(int stageNumber)
    {
        for (int i = 0; i < _allStage.Stages.Length; i++)
        {
            if (_allStage.Stages[i].StageNumber == stageNumber)
            {
                _allStage.Stages[i].StagePassed();
                _allStage.Stages[i + 1].SetUnlock();

                CreateNewStage(i + 2);
                Save();

                return;
            }
        }
    }
}

[Serializable] 
public class AllStage
{
    public Stage[] Stages;
}

[Serializable]
public class Stage
{
    public int StageNumber;
    public int Part;
    public bool Islocked;

    public Stage(int stage)
    {
        StageNumber = stage;

        Part = 1;
        Islocked = true;
    }

    public void StagePassed() => Part++;
    public void SetUnlock() => Islocked = false;
}