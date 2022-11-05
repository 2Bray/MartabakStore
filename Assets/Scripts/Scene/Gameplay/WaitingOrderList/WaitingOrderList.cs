using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingOrderList : MonoBehaviour
{
    private List<ICustomerOrder> _customerOrders;

    public void Setup()
    {
        _customerOrders = new List<ICustomerOrder>();
    }

    public void AddingToList(ICustomerOrder order)
    {
        _customerOrders.Add(order);
    }

    public void RemoveFromList(ICustomerOrder order)
    {
        _customerOrders.Remove(order);
    }

    public bool ServeToCustomer(Martabak martabak)
    {
        foreach(ICustomerOrder order in _customerOrders)
            if (order.CheckingOrder(martabak))
                return true;
                
        return false;
    }
}
