using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayLoader : SetupLoader
{
    [SerializeField] private TimerManager _timerManager;


    [SerializeField] private LevelDatabase _levelDatabase;
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private GameFlow _gameFlow;
    [SerializeField] private GameOverController _gameOverController;
    [SerializeField] private CustomerAreaController _customerAreaController;
    [SerializeField] private CustomerSpawner _customerSpawner;
    [SerializeField] private RaycastListener _raycastListener;

    [SerializeField] private WaitingOrderList _waitingOrderList;

    [SerializeField] private MartabakPool _martabakPool;
    [SerializeField] private DoughController _doughController;
    [SerializeField] private PanController _panController;
    [SerializeField] private PlateController _plateController;
    [SerializeField] private ToppingController _toppingController;


    public override void RunSetup()
    {
        _timerManager.Setup();

        _levelManager.Setup(_levelDatabase, _gameFlow);

        _gameOverController.Setup();
        _gameFlow.Setup(_gameOverController, _levelManager.TargetCustomer, _levelManager.IsStopWhenTarget);
        _raycastListener.Setup(_gameFlow);

        _waitingOrderList.Setup();

        _customerAreaController.Setup();

        _customerSpawner.Setup(
            _levelManager,
            _customerAreaController,
            _gameFlow,
            _waitingOrderList
            );

        _martabakPool.Setup();
        _plateController.Setup(_waitingOrderList);
        _panController.Setup(_plateController);
        _doughController.Setup(_martabakPool, _panController);

        _toppingController.Setup(_plateController);
    }
}
