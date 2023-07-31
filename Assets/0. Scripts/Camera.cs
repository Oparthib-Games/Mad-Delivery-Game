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
        offset.x = transform.position.x - player.position.x;
        offset.y = transform.position.y - player.position.y;
        offset.z = transform.position.z - player.position.z;
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
        Vector3 desiredPosition = new Vector3(
            transform.position.x + offset.x, transform.position.y, player.position.z + offset.z);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        //transform.position = desiredPosition;
    }
}
