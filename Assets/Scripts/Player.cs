using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float forwardSpeed = 5f;
    public float sideSpeed = 5f;

    Animator ANIM;

    GameManager gameManager;

    void Start()
    {
        ANIM = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Coin")
        {
            gameManager.onCoinCollide(other.gameObject);
        }
    }
}
