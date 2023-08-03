using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bike : MonoBehaviour
{
    public float forwardSpeed = 5f;
    public float sideSpeed = 5f;
    public float slideSpeed = 5f;
    public float slopeJumpForce = 5f;
    public float xClamp = 1;

    float forwardSpeedMultiplier = 1f;

    Animator ANIM;
    Rigidbody RB;

    GameManager gameManager;

    bool isSliding;
    bool isJumped;

    public GameObject[] Main_Smokes;
    public GameObject[] Booster_Smokes;

    public bool useManual;
    public bool useContiniousMovement = true;
    float manualH;
    float H;
    float horizontal;
    float vertical;

    [SerializeField] float nextStaticHPos;
    [SerializeField] int currLane;

    void Start()
    {
        ANIM = GetComponent<Animator>();
        RB = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        InputHandle();

        if (!gameManager.isGameStarted || gameManager.isGameOver) return;

        if(useContiniousMovement)
        {
            ContinousMovement();
        } else
        {
            StaticMovement();
        }

        Slide();
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        if (!gameManager.isGameStarted || gameManager.isGameOver) return;
    }

    void InputHandle()
    {
        if(useContiniousMovement)
        {
            H = useManual ? manualH : InputManager.H;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                Right();
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                Left();
            }
        }
    }

    public void Right()
    {
        H = 1;

        if (transform.position.x > -0.5f && transform.position.x < 0.5f)
        {
            nextStaticHPos = 1;
        }
        else if (transform.position.x < 0)
        {
            nextStaticHPos = 0;
        }
        else if (transform.position.x > 0)
        {
            nextStaticHPos = 1;
        }
    }

    public void Left()
    {
        H = -1;

        if (transform.position.x > -0.5f && transform.position.x < 0.5f)
        {
            nextStaticHPos = -1;
        }
        else if (transform.position.x < 0)
        {
            nextStaticHPos = -1;
        }
        else if (transform.position.x > 0)
        {
            nextStaticHPos = 0;
        }
    }

    void StaticMovement()
    {
        //if(Mathf.Abs(transform.position.x - nextStaticHPos) <= 0.12f)
        //{
        //    H = 0;
        //}
        if(nextStaticHPos != 0)
        {
            if (currLane == nextStaticHPos)
            {
                H = 0;
            }
        } else
        {
            if (currLane == nextStaticHPos && Mathf.Abs(transform.position.x - nextStaticHPos) <= 0.15f)
            {
                H = 0;
            }
        }

        horizontal = sideSpeed * H * 2;
        vertical = forwardSpeed * forwardSpeedMultiplier;
        Vector3 movement = new Vector3(horizontal, 0f, vertical) * Time.deltaTime;
        transform.Translate(movement);
    }

    void ContinousMovement()
    {
        //transform.Translate(Vector3.forward * forwardSpeed * forwardSpeedMultiplier * Time.deltaTime);
        //transform.Translate(Vector3.right * sideSpeed * InputManager.H * Time.deltaTime);


        horizontal = sideSpeed * H;
        vertical = forwardSpeed * forwardSpeedMultiplier;
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
        ANIM.SetFloat("Lean", H);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Center Lane"))
        {
            currLane = 0;
        }
        else if (other.gameObject.CompareTag("Right Lane"))
        {
            currLane = 1;
        }
        else if (other.gameObject.CompareTag("Left Lane"))
        {
            currLane = -1;
        }


        if (other.gameObject.CompareTag("Coin"))
        {
            gameManager.onCoinCollide(1, other.gameObject);
        }
        else if (other.gameObject.CompareTag("Cheez Coin"))
        {
            gameManager.onCoinCollide(5, other.gameObject);
        }
        else if (other.gameObject.CompareTag("Booster"))
        {
            gameManager.onBoosterCollide(other.gameObject);

            StartCoroutine(BoosterEffect());
        }
        else if (other.gameObject.CompareTag("Lane Trigger"))
        {
            Destroy(other.gameObject);
            gameManager.SpawnLane();
        }
        else if (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("Car"))
        {
            gameManager.GameOver();
        }
        else if (other.gameObject.CompareTag("Slope"))
        {
            SlopeJump();
        }
        else if (other.gameObject.CompareTag("Fuel"))
        {
            gameManager.AddFuel(other.gameObject, 100);
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

    public void ManualInput(int value)
    {
        manualH = value;
    }
}
