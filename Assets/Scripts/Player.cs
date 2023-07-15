using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float forwardSpeed = 5f;
    public float sideSpeed = 5f;
    public float slideSpeed = 5f;
    public float slopeJumpForce = 5f;

    Animator ANIM;
    Rigidbody RB;

    GameManager gameManager;

    bool isSliding;
    bool isJumped;

    void Start()
    {
        ANIM = GetComponent<Animator>();
        RB = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (!gameManager.isGameStarted || gameManager.isGameOver) return;

        Movement();
        Slide();
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
        if(other.gameObject.tag == "Lane Trigger")
        {
            Destroy(other.gameObject);
            gameManager.SpawnLane();
        }
        if(other.gameObject.tag == "Obstacle" || other.gameObject.tag == "Car")
        {
            gameManager.GameOver();
        }
        if (other.gameObject.tag == "Slope")
        {
            SlopeJump();
        }
    }

    void Slide()
    {
        if (InputManager.slidePerformed && !isSliding)
        {
            isSliding = true;
            ANIM.SetTrigger("Slide");
        }
    }
    public void SlideEndAnimEvent()
    {
        isSliding = false;
    }
    void SlopeJump()
    {
        if(!isJumped)
        {
            Debug.Log("SlopeJump");
            RB.AddForce(new Vector3(0, 1, 1) * slopeJumpForce);
            isJumped = true;
        }
    }
}
