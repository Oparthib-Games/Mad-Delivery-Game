using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int coinCount = 0;

    public GameObject coinParticle;
    public AudioClip coinSound;
    public AudioClip gameoverSound;

    public TextMeshProUGUI coinTMPro;
    public TextMeshProUGUI finalScoreTMPro;
    public TextMeshProUGUI startingCounterTMPro;
    public GameObject gameOverPanel;

    Camera camera;

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

        startingCounterTMPro.gameObject.SetActive(true);
        gameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onCoinCollide(GameObject coinGO)
    {
        Instantiate(coinParticle, coinGO.gameObject.transform.position, Random.rotation);
        Destroy(coinGO.gameObject);
        AudioSource.PlayClipAtPoint(coinSound, camera.transform.position, 0.5f);
        coinCount++;
        coinTMPro.text = coinCount.ToString();
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

    public void GameOver()
    {
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
}
