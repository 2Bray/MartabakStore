using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerAreaController : MonoBehaviour
{
    [SerializeField] private SpotArea[] _areas;

    public void Setup()
    {
        foreach (SpotArea area in _areas)
            area.Setup();
    }

    public SpotArea FoundFreeArea()
    {
        foreach(SpotArea area in _areas)
        {
            if (area.RequestAreaAllowed())
            {
                return area;
            }
        }

        return null;
    }
}
