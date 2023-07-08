using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float forwardSpeed = 5f;
    public float sideSpeed = 5f;

    Animator ANIM;

    void Start()
    {
        ANIM = GetComponent<Animator>();
    }

    void Update()
    {
        Movement();
        UpdateAnimation();
    }

    void Movement()
    {
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
        transform.Translate(Vector3.right * sideSpeed * InputManager.H * Time.deltaTime);
    }

    void UpdateAnimation()
    {
        ANIM.SetFloat("Lean", InputManager.H);
    }
}
