using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionFlow : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadScene(MoveSceneRequest.Instance.NextScene);
    }
}
