using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int coinCount = 0;

    public GameObject coinParticle;
    public AudioClip coinSound;

    Camera camera;

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
    }
}
