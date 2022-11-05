using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToppingController : MonoBehaviour
{
    [SerializeField] private BasedTopping[] _toppings;

    public void Setup(PlateController plateController)
    {
        foreach (BasedTopping topping in _toppings)
            topping.Setup(plateController);
    }
}
