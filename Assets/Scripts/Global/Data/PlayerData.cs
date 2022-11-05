using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private static PlayerData _instance;
    public static PlayerData Instance { get => _instance; }

    public StageData StageData { get => _stageData; }
    private StageData _stageData;

    public void Setup()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);

            LoadData();
        }
        else if (_instance != this)
            Destroy(gameObject);
    }

    public void LoadData()
    {
        _stageData = new StageData().Load();
    }
}
