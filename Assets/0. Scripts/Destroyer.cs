using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("SSSSSSSSSSSSSSSSSSSSSS");
        Destroy(other.gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OOOOOOOOOOOOOOOOOOOOOOOOO");
        Destroy(collision.gameObject);
    }
}
