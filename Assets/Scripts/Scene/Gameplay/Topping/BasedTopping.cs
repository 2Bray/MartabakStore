using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasedTopping : MonoBehaviour, IClickable
{
    protected PlateController _plateController;
    [SerializeField] protected string _topping;


    public void Setup(PlateController plateController)
    {
        _plateController = plateController;
    }

    public void OnClicked()
    {
        GiveTopping(_topping);
    }

    private void GiveTopping(string topping)
    {
        PutTopping();

        _plateController.GivingTopping(topping);
    }

    protected abstract void PutTopping();
}