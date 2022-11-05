using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MartabakController : BasedPoolingObject
{
    [SerializeField] private GameObject _chocoTopping;
    [SerializeField] private GameObject _cheeseTopping;
    [SerializeField] private GameObject _pandanTopping;

    [SerializeField] private Color _doneColor;
    private SpriteRenderer _spriteRenderer;

    public Martabak Martabak { get => _martabak; }
    private Martabak _martabak;

    
    public override void Setup()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _martabak = new Martabak();
        gameObject.SetActive(false);
    }

    public override bool Spawn()
    {
        if (gameObject.activeInHierarchy) 
            return false;

        _spriteRenderer.color = Color.white;
        _martabak.Reset();
        
        gameObject.SetActive(true);

        return true;
    }

    public void SetPosition(Vector2 pos) => transform.position = pos;

    public void MartabakDone() => _spriteRenderer.color = _doneColor;

    public void OverCook()
    {
        _spriteRenderer.color = Color.black;
        _martabak.SetOverCook();
    }

    public bool GiveTopping(string topping)
    {
        if (_martabak.IsOverCook || _martabak.Topping != Martabak.Base)
            return false;

        if (topping == Martabak.Choco)
        {
            _martabak.SetTopping(Martabak.Choco);
            _chocoTopping.SetActive(true);
        }
        else if (topping == Martabak.Cheese)
        {
            _martabak.SetTopping(Martabak.Cheese);
            _cheeseTopping.SetActive(true);
        }
        else if (topping == Martabak.Pandan)
        {
            _martabak.SetTopping(Martabak.Pandan);
            _pandanTopping.SetActive(true);
        }

        return true;
    }

    public void ThrowToTrash() => Deactive();

    public void ServeMartabakToCostumer() => Deactive();

    protected override void Deactive()
    {
        _chocoTopping.SetActive(false);
        _cheeseTopping.SetActive(false);
        _pandanTopping.SetActive(false);

        gameObject.SetActive(false);
    }
}

public class Martabak
{
    public static int VariantCount { get => _toppingVariant.Length; }
    private static string[] _toppingVariant = { "None", "Choco", "Cheese", "Pandan" };
    
    public string Base { get => _toppingVariant[0]; }
    public string Choco { get => _toppingVariant[1]; }
    public string Cheese { get => _toppingVariant[2]; }
    public string Pandan { get => _toppingVariant[3]; }

    public string Random { get => 
            _toppingVariant[UnityEngine.Random.Range(0, VariantCount)];
    }

    public string Topping { get => _currentTopping; }
    private string _currentTopping;

    public bool IsOverCook { get => _isOverCook; }
    private bool _isOverCook;

    public bool Compare(Martabak other)
    {
        return _currentTopping == other._currentTopping;
    }

    public void SetTopping(string topping) => _currentTopping = topping;
    public void SetOverCook() => _isOverCook = true;
    public void Reset()
    {
        _isOverCook = false;
        _currentTopping = Base;
    }
}