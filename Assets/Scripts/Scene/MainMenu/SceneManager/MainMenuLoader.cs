using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLoader : SetupLoader
{
    [SerializeField] private StageController _stageController;

    public override void RunSetup()
    {
        _stageController.Setup();
    }
}