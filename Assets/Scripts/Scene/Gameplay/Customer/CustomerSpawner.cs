using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : BasedPoolingClass<CustomerController>
{
    private CustomerAreaController _customerAreaController;
    private ICustomerGetServeSubcriber _customerSubscirber;
    private WaitingOrderList _waitingOrderList;

    [SerializeField] private float _spawnTimeRange;
    private float _timeSpawn;

    [SerializeField] private Vector2[] _spawnPoint;

    private TimerManager _timerManager;

    private int _maxOrder;


    public void Setup(
        LevelManager levelManager,
        CustomerAreaController customerAreaController,
        ICustomerGetServeSubcriber customrSubcriber,
        WaitingOrderList waitingOrderList
        )
    {
        _customerAreaController = customerAreaController;
        _customerSubscirber = customrSubcriber;
        _waitingOrderList = waitingOrderList;

        _maxOrder = levelManager.MaxCustomerOrder;

        base.Setup();

        _timeSpawn = 3;
        _timerManager = TimerManager.Instance;

        _timerManager.Alarm += Spawn;
    }

    public void SetMaxCustomerOrder(int max) => _maxOrder = max;

    public void Spawn(float time)
    {
        if (time >= _timeSpawn)
        {
            _timeSpawn = time + _spawnTimeRange;

            SpotArea freeArea = _customerAreaController.FoundFreeArea();
            if (freeArea)
            {
                CustomerController customer = base.SpawnObject();

                customer.SetPosition(RandomSpawnPoint());
                customer.SetSpotArea(freeArea);
            }
        }
    }

    private Vector2 RandomSpawnPoint()
        => _spawnPoint[Random.Range(0, _spawnPoint.Length)];

    protected override void OnNewObjectAdding(CustomerController obj)
    {
        obj.SetSubcriberOnGetServe(_customerSubscirber);
        obj.SetCustomerOrder(_waitingOrderList, _maxOrder);
    }
}
