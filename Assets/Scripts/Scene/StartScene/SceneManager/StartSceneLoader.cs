using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneLoader : SetupLoader
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private MoveSceneRequest _moveSceneRequest;
    [SerializeField] private StartSceneFlow _startSceneFlow;

    public override void RunSetup()
    {
        _playerData.Setup();
        _moveSceneRequest.Setup();

        _startSceneFlow.Setup(this);
    }
}