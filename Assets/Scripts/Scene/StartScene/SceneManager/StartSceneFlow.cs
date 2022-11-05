using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneFlow : MonoBehaviour
{
    public void Setup(StartSceneLoader startSceneLoader) =>
        startSceneLoader.OnLoadCompleted += OnLoadCompleted;

    private void OnLoadCompleted() => MoveSceneRequest.Instance.MoveTo("MainMenu");
}
