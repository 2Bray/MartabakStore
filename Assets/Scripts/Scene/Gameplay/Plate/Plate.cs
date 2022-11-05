using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour, IClickable, IPlate
{
    private WaitingOrderList _waitingOrderList;
    private MartabakController _currentMartabak;

    private bool _isHaveMartabak;

    public void Setup(WaitingOrderList waitingOrderList)
    {
        _waitingOrderList = waitingOrderList;

        _isHaveMartabak = false;
    }

    public void OnClicked()
    {
        if (!_isHaveMartabak)
            return;

        ServeToCustomer();
    }

    public void ServeToPlate(MartabakController martabak)
    {
        _isHaveMartabak = true;

        _currentMartabak = martabak;
        _currentMartabak.SetPosition(transform.position);
    }

    private void ServeToCustomer()
    {
        if (_currentMartabak.Martabak.IsOverCook)
        {
            _currentMartabak.ThrowToTrash();
            _isHaveMartabak = false;
        }

        else if (_waitingOrderList.ServeToCustomer(_currentMartabak.Martabak))
        {
            _currentMartabak.ServeMartabakToCostumer();
            _isHaveMartabak = false;
        }
    }

    public bool AddingTopping(string topping)
    {
        if (!_isHaveMartabak)
            return false;

        return _currentMartabak.GiveTopping(topping);
    }

    public bool IsHaveMartabak() => _isHaveMartabak;
}
