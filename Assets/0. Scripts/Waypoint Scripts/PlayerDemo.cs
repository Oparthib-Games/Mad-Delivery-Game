using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDemo : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Start()
    {
        
    }

    void Update()
    {
        Debug.Log(InputManager.H);
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        Vector3 movementDirection = new Vector3(InputManager.H, 0f, InputManager.V).normalized;
        Vector3 movement = movementDirection * moveSpeed * Time.fixedDeltaTime;

        transform.position += movement;
    }
}
