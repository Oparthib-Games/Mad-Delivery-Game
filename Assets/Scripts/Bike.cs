using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bike : MonoBehaviour
{
    public float forwardSpeed = 5f;
    public float sideSpeed = 5f;
    public float slideSpeed = 5f;
    public float slopeJumpForce = 5f;

    float forwardSpeedMultiplier = 1f;

    Animator ANIM;
    Rigidbody RB;

    GameManager gameManager;

    bool isSliding;
    bool isJumped;

    public GameObject[] Main_Smokes;
    public GameObject[] Booster_Smokes;

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

    private void FixedUpdate()
    {
        if (!gameManager.isGameStarted || gameManager.isGameOver) return;
    }

    void Movement()
    {
        //transform.Translate(Vector3.forward * forwardSpeed * forwardSpeedMultiplier * Time.deltaTime);
        //transform.Translate(Vector3.right * sideSpeed * InputManager.H * Time.deltaTime);


        float horizontal = sideSpeed * InputManager.H;
        float vertical = forwardSpeed * forwardSpeedMultiplier;
        Vector3 movement = new Vector3(horizontal, 0f, vertical) * Time.deltaTime;
        transform.Translate(movement);

        //float delta = Time.deltaTime;
        //Vector3 currPos = transform.position;
        //currPos.z += forwardSpeed * forwardSpeedMultiplier * delta;
        //currPos.x += sideSpeed * InputManager.H * delta;
        //transform.position = currPos;
    }

    void UpdateAnimation()
    {
        ANIM.SetFloat("Lean", InputManager.H);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Coin"))
        {
            gameManager.onCoinCollide(other.gameObject);
        }
        if(other.gameObject.CompareTag("Booster"))
        {
            gameManager.onBoosterCollide(other.gameObject);

            StartCoroutine(BoosterEffect());
        }
        if(other.gameObject.CompareTag("Lane Trigger"))
        {
            Destroy(other.gameObject);
            gameManager.SpawnLane();
        }
        if(other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("Car"))
        {
            gameManager.GameOver();
        }
        if (other.gameObject.CompareTag("Slope"))
        {
            SlopeJump();
        }
    }

    IEnumerator BoosterEffect()
    {
        ActiveBoosterSmoke();
        forwardSpeedMultiplier = 2f;

        yield return new WaitForSeconds(3f);

        forwardSpeedMultiplier = 1f;
        InactiveBoosterSmoke();
    }

    void ActiveBoosterSmoke()
    {
        foreach (GameObject item in Main_Smokes)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in Booster_Smokes)
        {
            item.SetActive(true);
        }
    }
    void InactiveBoosterSmoke()
    {
        foreach (GameObject item in Main_Smokes)
        {
            item.SetActive(true);
        }
        foreach (GameObject item in Booster_Smokes)
        {
            item.SetActive(false);
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
