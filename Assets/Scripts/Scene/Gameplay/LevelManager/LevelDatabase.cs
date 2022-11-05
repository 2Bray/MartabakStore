using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDatabase : MonoBehaviour
{
    [SerializeField] private StageSetting[] StageSetting;

    public StageSetting GetStageByNumber(int partNumber)
    {
        foreach (StageSetting stage in StageSetting)
            if (stage.StageNumber == partNumber)
                return stage;

        return null;
    }
}

[System.Serializable]
public class StageSetting
{
    public int StageNumber;
    public PartSetting[] Parts;

    public PartSetting GetPartByNumber(int partNumber)
    {
        foreach (PartSetting part in Parts)
            if (part.PartNumber == partNumber)
                return part;

        return null;
    }
}

[System.Serializable]
public class PartSetting
{
    public int PartNumber;

    public int CustomerTargetCount;
    public int MaxCustomerOrder;
    public float GameDuration;
    public bool IsStopWhenTarget;
}