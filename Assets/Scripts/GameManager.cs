using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int coinCount = 0;
    [SerializeField] private float fuel = 100;
    [SerializeField] private float fuelDecreaseRate = 5f;

    public GameObject coinParticle;
    public AudioClip coinSound;
    public GameObject boosterParticle;
    public AudioClip boosterSound;
    public GameObject fuelParticle;
    public AudioClip gameoverSound;

    public TextMeshProUGUI coinTMPro;
    public TextMeshProUGUI finalScoreTMPro;
    public TextMeshProUGUI startingCounterTMPro;
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverTextTMPro;
    public GameObject startPanel;
    public GameObject fuelSliderGO;
    Slider fuelSlider;
    Image slideFillImage;

    Camera camera;

    public Color sliderBase;
    public Color sliderYellow;
    public Color sliderRed;

    public bool isPlayClicked;
    public bool isGameStarted;
    public bool isGameOver;
    public int startingCounter = 3;

    public List<GameObject> lanes;
    public List<GameObject> activeLanes;
    public GameObject laneParent;
    float nextLaneZPos = 100;

    void Start()
    {
        camera = FindObjectOfType<Camera>();
        fuelSlider = fuelSliderGO.GetComponent<Slider>();
        slideFillImage = fuelSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>();


        gameOverPanel.SetActive(false);
        fuelSliderGO.SetActive(false);
        startPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(fuel > 50)
        {
            slideFillImage.color = sliderBase;
        } else if(fuel > 20)
        {
            slideFillImage.color = sliderYellow;
        } else
        {
            slideFillImage.color = sliderRed;
        }

        if(fuel <= 0 && !isGameOver)
        {
            GameOver("fuel");
        }
    }

    public void onPlayClick()
    {
        isPlayClicked = true;
        startingCounterTMPro.gameObject.SetActive(true);
        startPanel.SetActive(false);
        fuelSliderGO.SetActive(true);
        StartCoroutine(DecreaseFuel());
    }

    public void onCoinCollide(GameObject coinGO)
    {
        Instantiate(coinParticle, coinGO.gameObject.transform.position, Random.rotation);
        Destroy(coinGO.gameObject);
        AudioSource.PlayClipAtPoint(coinSound, camera.transform.position, 0.5f);
        coinCount++;
        coinTMPro.text = coinCount.ToString();
    }
    public void onBoosterCollide(GameObject boosterGO)
    {
        Instantiate(boosterParticle, boosterGO.gameObject.transform.position, Random.rotation);
        Destroy(boosterGO.gameObject);
        AudioSource.PlayClipAtPoint(boosterSound, camera.transform.position, 0.7f);

        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject item in obstacles)
        {
            item.SetActive(false);
        }
        GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");
        foreach (GameObject item in cars)
        {
            item.SetActive(false);
        }
    }

    public void CounterUpdate()
    {
        startingCounter--;
        startingCounterTMPro.text = startingCounter.ToString();

        if (startingCounter <= 0)
        {
            isGameStarted = true;
            startingCounterTMPro.gameObject.SetActive(false);
        }
    }

    public void SpawnLane()
    {
        Destroy(activeLanes[0]);
        for (int i = 0; i < activeLanes.Count - 1; i++)
        {
            activeLanes[i] = activeLanes[i + 1];
        }
        activeLanes.RemoveAt(activeLanes.Count - 1);

        int randomIndex = Random.Range(0, lanes.Count);
        GameObject laneGO = Instantiate(lanes[randomIndex], new Vector3(0, 0, nextLaneZPos), Quaternion.identity) as GameObject;
        laneGO.transform.SetParent(laneParent.transform);
        activeLanes.Add(laneGO);
        nextLaneZPos += 20;
    }

    public void GameOver(string gameover_type = "basic")
    {
        if(gameover_type == "fuel")
        {
            gameOverTextTMPro.text = "Out of Fuel!!";
        }

        isGameOver = true;
        AudioSource.PlayClipAtPoint(gameoverSound, camera.transform.position, 0.5f);
        finalScoreTMPro.text = coinCount.ToString();
        gameOverPanel.SetActive(true);
    }

    public void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    IEnumerator DecreaseFuel()
    {
        fuel -= fuelDecreaseRate;
        UpdateFuelSlider();
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(DecreaseFuel());
    }

    public void AddFuel(GameObject fuelGO, float fuelAmount)
    {
        Instantiate(fuelParticle, fuelGO.gameObject.transform.position, Random.rotation);
        Destroy(fuelGO.gameObject);
        AudioSource.PlayClipAtPoint(boosterSound, camera.transform.position, 0.7f);

        fuel += fuelAmount;
        if (fuel > 100) fuel = 100;
        UpdateFuelSlider();
    }

    void UpdateFuelSlider()
    {
        float fuelSliderValue = fuel / 100;
        fuelSlider.value = fuelSliderValue;
    }
}
