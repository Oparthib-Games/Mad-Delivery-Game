using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeCtrl : MonoBehaviour
{
    public Vector3 touchStartPos;
    public Vector3 touchEndPos;

    GameManager gameManager;
    Bike bikeScript;

    public bool isMouseTouch;

    private void Start()
    {
        gameManager = GetComponent<GameManager>();
        bikeScript = FindObjectOfType<Bike>();
    }

    private void Update()
    {
        if(!gameManager.isGameStarted)
        {
            return;
        }

        if(isMouseTouch)
        {
            if (Input.GetMouseButtonDown(0))
            {
                touchStartPos = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                touchEndPos = Input.mousePosition;
                ApplySwipe();
            }
        }
        else
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touchStartPos = Input.GetTouch(0).position;
            }
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                touchEndPos = Input.GetTouch(0).position;
                ApplySwipe();
            }
        }
    }

    void ApplySwipe()
    {
        if(touchStartPos.x < touchEndPos.x)
        {
            bikeScript.Right();
        } else if (touchStartPos.x > touchEndPos.x)
        {
            bikeScript.Left();
        }
    }
}
