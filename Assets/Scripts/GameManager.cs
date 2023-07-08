using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int coinCount = 0;

    public GameObject coinParticle;
    public AudioClip coinSound;

    public TextMeshProUGUI coinTMPro;
    public TextMeshProUGUI startingCounterTMPro;

    Camera camera;

    public bool isGameStarted;
    public int startingCounter = 3;

    void Start()
    {
        camera = FindObjectOfType<Camera>();
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
}
