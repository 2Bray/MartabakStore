using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasedPoolingObject : MonoBehaviour
{
    public abstract void Setup();
    public abstract bool Spawn();
    protected abstract void Deactive();
}
