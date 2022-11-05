using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastListener : MonoBehaviour
{
    private Camera _mainCam;

    public void Setup(GameFlow gameFlow)
    {
        gameFlow.OnGameOver += () => gameObject.SetActive(false);

        _mainCam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(_mainCam.ScreenToWorldPoint(Input.mousePosition), -Vector3.back);

            if (hit)
            {
                IClickable obj = hit.collider.GetComponent<IClickable>();

                if (obj != null)
                    obj.OnClicked();
            };
        }
    }
}