using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerOrderUI : MonoBehaviour
{
    [SerializeField] private Slider _timeSlider;
    [SerializeField] private Image _sliderProgress;

    [Header("Martabak")]
    [SerializeField] private GameObject _martabakBase;
    [SerializeField] private GameObject _martabakChoco;
    [SerializeField] private GameObject _martabakCheese;
    [SerializeField] private GameObject _martabakPandan;

    private GameObject[] _martabakBasePool;
    private GameObject[] _martabakChocoPool;
    private GameObject[] _martabakCheesePool;
    private GameObject[] _martabakPandanPool;

    private GameObject[] _currentOrder;
    private Camera cam;

    public void Setup(float timeMaxValue)
    {
        gameObject.SetActive(false);
        
        cam = Camera.main;
        _timeSlider.maxValue = timeMaxValue;
    }

    public void SetPoolMaxSize(int maxOrder)
    {
        _martabakBasePool = GeneratePoolItem(_martabakBase, maxOrder);
        _martabakChocoPool = GeneratePoolItem(_martabakChoco, maxOrder);
        _martabakCheesePool = GeneratePoolItem(_martabakCheese, maxOrder);
        _martabakPandanPool = GeneratePoolItem(_martabakPandan, maxOrder);
    }

    private GameObject[] GeneratePoolItem(GameObject template, int size)
    {
        GameObject[] pool = new GameObject[size];

        pool[0] = template;

        for (int i = 1; i < pool.Length; i++)
        {
            pool[i] = Instantiate(template, template.transform.parent);
        }

        return pool;
    }

    public void ShowOrder(Vector2 worldPos, Martabak[] order)
    {
        transform.position = cam.WorldToScreenPoint(worldPos);

        SetCustomerOrder(order);
        ResetTimeSlider();

        gameObject.SetActive(true);
    }

    private void SetCustomerOrder(Martabak[] order)
    {
        _currentOrder = new GameObject[order.Length];

        for (int i = 0; i < order.Length; i++)
        {
            if (order[i].Topping == order[i].Base)
                _currentOrder[i] = GetMartabakUI(_martabakBasePool);

            else if (order[i].Topping == order[i].Choco)
                _currentOrder[i] = GetMartabakUI(_martabakChocoPool);

            else if (order[i].Topping == order[i].Cheese)
                _currentOrder[i] = GetMartabakUI(_martabakCheesePool);

            else if (order[i].Topping == order[i].Pandan)
                _currentOrder[i] = GetMartabakUI(_martabakPandanPool);

            else
            {
                Debug.LogError("Adding Martabak " + order[i].Topping + " In CustomerOrderUI Prefabs");
                return;
            }

            _currentOrder[i].SetActive(true);
        }
    }

    private GameObject GetMartabakUI(GameObject[] pool)
    {
        foreach (GameObject item in pool)
            if (!item.activeSelf)
                return item;

        return null;
    }

    public void SetTimeSlider(float value)
    {
        _timeSlider.value = value;

        if (_timeSlider.value <= _timeSlider.maxValue * 0.3f)
            _sliderProgress.color = Color.red;
    }

    private void ResetTimeSlider()
    {
        _sliderProgress.color = Color.green;
        _timeSlider.value = _timeSlider.maxValue;
    }

    public void ShowOffMartabak(int index)
        => _currentOrder[index].SetActive(false);

    public void Deactive()
    {
        foreach (GameObject item in _currentOrder)
            item.SetActive(false);

        gameObject.SetActive(false);
    }
}