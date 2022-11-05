using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : BasedPoolingObject
{
    public Action OnGetServe;
    
    [SerializeField] private float movementSpeed;

    private SpriteRenderer _spriteRenderer;
    private CustomerOrder _customerOrder;

    private bool _isMove;
    private bool _isDone;

    private SpotArea _spotArea;

    private Vector2 _startPosition;
    private Vector2 _nextDestiny;


    private void Update()
    {
        if (_isMove)
        {
            transform.position = Vector2.Lerp(transform.position, _nextDestiny,
                (movementSpeed * 0.01f) / Vector2.Distance(transform.position, _nextDestiny));

            if ((Vector2)transform.position == _nextDestiny)
            {
                _spriteRenderer.sortingOrder = 0;

                if (_isDone)
                    Deactive();
                else
                    Order();
            }
        }
    }

    public override void Setup()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _customerOrder = GetComponent<CustomerOrder>();
        _customerOrder.OnDoneOrder += GoBack;
        
        Deactive();
    }

    public void SetCustomerOrder(WaitingOrderList waitingOrderList, int maxOrder)
    {
        _customerOrder.Setup(waitingOrderList, maxOrder);
    }

    public override bool Spawn()
    {
        if (gameObject.activeInHierarchy)
            return false;

        _isMove = false;
        _isDone = false;

        gameObject.SetActive(true);

        return true;
    }

    public void SetSubcriberOnGetServe(ICustomerGetServeSubcriber subcriber) =>
        OnGetServe += subcriber.OnCustomerGetServe;

    public void SetPosition(Vector2 pos)
    {
        _startPosition = pos;
        transform.position = _startPosition;
    }

    public void SetSpotArea(SpotArea spot)
    {
        _spotArea = spot;
        StartMovingTo(spot.Position);
    }

    private void StartMovingTo(Vector2 destiny)
    {
        _nextDestiny = destiny;

        _spriteRenderer.sortingOrder = -1;
        _isMove = true;
    }

    private void Order()
    {
        _isMove = false;
        _customerOrder.CustomerStartOrder();
    }

    private void GoBack(bool isGetTheOrder)
    {
        if (isGetTheOrder)
            OnGetServe();

        _isDone = true;

        _spotArea.SetFree();
        StartMovingTo(_startPosition);
    }

    protected override void Deactive()
    {
        gameObject.SetActive(false);
    }
}
