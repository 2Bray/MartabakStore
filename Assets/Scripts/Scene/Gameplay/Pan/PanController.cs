using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanController : MonoBehaviour
{
    [SerializeField] private Pan[] _pans;


    public void Setup(PlateController plateController)
    {
        foreach (Pan pan in _pans)
            pan.Setup(plateController);
    }

    public IPan RequestPan()
    {
        foreach (IPan pan in _pans)
            if (pan.IsFree())
                return pan;

        return null;
    }
}
