using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateController : MonoBehaviour
{
    [SerializeField] private Plate[] _plates;


    public void Setup(WaitingOrderList waitingOrderList)
    {
        foreach (Plate plate in _plates)
            plate.Setup(waitingOrderList);
    }

    public IPlate RequestPlate()
    {
        foreach (IPlate plate in _plates)
            if (!plate.IsHaveMartabak())
                return plate;

        return null;
    }

    public void GivingTopping(string topping)
    {
        foreach (IPlate plate in _plates)
            if (plate.AddingTopping(topping))
                break;
    }
}
