using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void Start()
    {
        
    }

    void Update()
    {
        FollowTarget();
    }

    private void FixedUpdate()
    {
        
    }

    void FollowTarget()
    {
        Vector3 desiredPosition = new Vector3(transform.position.x + offset.x, transform.position.y, player.position.z + offset.z);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
