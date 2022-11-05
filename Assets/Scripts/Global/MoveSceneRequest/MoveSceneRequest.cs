using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MoveSceneRequest : MonoBehaviour
{
    private static MoveSceneRequest _instance;
    public static MoveSceneRequest Instance { get => _instance; }

    public string NextScene { get => _nextScene; }
    private string _nextScene;

    public int Stage { get => _stage; }
    private int _stage;

    public void Setup()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
            Destroy(gameObject);
    }

    public void MoveTo(string sceneName)
    {
        _nextScene = sceneName;
        SceneManager.LoadScene("Transition");
    }

    public void SetStage(int stageNumber) => _stage = stageNumber;
}
