using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasedPoolingClass<T> : MonoBehaviour where T : BasedPoolingObject
{
    [SerializeField] private T _obj;
    [SerializeField] private int _size;

    protected List<T> _pool;

    public void Setup()
    {
        _pool = new List<T>();

        for (int i = 0; i < _size; i++)
        {
            InstantiateObject();
        }
    }

    public T SpawnObject()
    {
        foreach (T obj in _pool)
        {
            if (obj.Spawn())
                return obj;
        }

        T t = InstantiateObject();
        t.Spawn();

        return t;
    }

    private T InstantiateObject()
    {
        T obj = Instantiate(_obj, transform);

        obj.Setup();
        _pool.Add(obj);

        OnNewObjectAdding(obj);

        return obj;
    }

    protected abstract void OnNewObjectAdding(T obj);
}
