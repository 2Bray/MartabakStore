using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotArea : MonoBehaviour
{
    public Vector2 Position { get => _position; }
    private Vector2 _position;

    private bool _isFree;


    public void Setup()
    {
        _position = transform.position;
        _isFree = true;
    }

    public bool RequestAreaAllowed()
    {
        if (!_isFree) 
            return false;

        _isFree = false;
        return true;
    }

    public void SetFree() => _isFree = true;
}
