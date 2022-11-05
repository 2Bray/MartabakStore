using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SetupLoader : MonoBehaviour
{
    public Action OnLoadCompleted;

    private void Start()
    {
        RunSetup();
        OnLoadCompleted?.Invoke();
    }

    public abstract void RunSetup();
}
