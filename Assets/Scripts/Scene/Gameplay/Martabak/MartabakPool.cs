using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MartabakPool : BasedPoolingClass<MartabakController>, IMartabakSpawner
{
    public void Setup()
    {
        base.Setup();
    }

    public MartabakController SpawnMartabak()
    {
        return base.SpawnObject();
    }

    protected override void OnNewObjectAdding(MartabakController obj)
    {
        
    }
}
