using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoughController : MonoBehaviour, IClickable
{
    private IMartabakSpawner _martabakSpawner;
    private PanController _panController;


    public void Setup(MartabakPool martabakPool, PanController panController)
    {
        _martabakSpawner = martabakPool;
        _panController = panController;
    }

    public void OnClicked()
    {
        IPan pan = _panController.RequestPan();

        if (pan != null)
        {
            pan.Cooking(_martabakSpawner.SpawnMartabak());
        }
    }
}