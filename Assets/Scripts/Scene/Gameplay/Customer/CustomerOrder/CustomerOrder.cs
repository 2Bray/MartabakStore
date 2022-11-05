using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerOrder : MonoBehaviour, ICustomerOrder
{
    public Action<bool> OnDoneOrder;

    private Martabak[] _martabak;

    [SerializeField] private CustomerOrderUI _orderUITemplate;
    [SerializeField] private Transform _orderUISpawnPosition;
    [SerializeField] private float _waitingTime;

    private int _maxOrderCount;

    private CustomerOrderUI _orderUI;
    private float _overTime;

    private TimerManager _timerManger;
    private WaitingOrderList _waitingList;

    private bool _isGetTheOreder;

    public void Setup(WaitingOrderList waitingList, int maxOrder)
    {
        _timerManger = TimerManager.Instance;
        _waitingList = waitingList;

        _orderUI = Instantiate(_orderUITemplate, FindObjectOfType<CustomerOrderUIParent>().transform);
        _orderUI.Setup(_waitingTime);

        _maxOrderCount = maxOrder;
        _orderUI.SetPoolMaxSize(_maxOrderCount);
    }

    public void CustomerStartOrder()
    {
        SetOrder();

        _overTime = _waitingTime + _timerManger.CurrentTime;
        _timerManger.Alarm += SetAlarm;
    }

    private void SetOrder()
    {
        _isGetTheOreder = false;

        _martabak = new Martabak[UnityEngine.Random.Range(0, _maxOrderCount) + 1];

        for (int i = 0; i < _martabak.Length; i++)
        {
            _martabak[i] = new Martabak();
            _martabak[i].SetTopping(_martabak[i].Random);
        }

        _orderUI.ShowOrder(_orderUISpawnPosition.position, _martabak);
        _waitingList.AddingToList(this);
    }

    private void SetAlarm(float Time)
    {
        _orderUI.SetTimeSlider(_overTime - Time);

        if (Time >= _overTime)
        {
            OnDone();
        }
    }

    private void OnDone()
    {
        _timerManger.Alarm -= SetAlarm;
        _waitingList.RemoveFromList(this);

        _orderUI.Deactive();
        OnDoneOrder(_isGetTheOreder);
    }

    public bool CheckingOrder(Martabak martabak)
    {
        for(int i = 0; i<_martabak.Length; i++)
        {
            if (_martabak[i] == null) continue;

            if (_martabak[i].Compare(martabak))
            {
                _orderUI.ShowOffMartabak(i);
                _martabak[i] = null;

                if (IsOrderComplete())
                {
                    _isGetTheOreder = true;
                    OnDone();
                }

                return true;
            }
        }

        return false;
    }

    private bool IsOrderComplete()
    {
        for(int i = 0; i < _martabak.Length; i++)
        {
            if (_martabak[i] != null) return false;
        }

        return true;
    }
}
