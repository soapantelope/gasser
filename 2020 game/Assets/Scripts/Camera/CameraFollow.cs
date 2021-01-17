using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float camSpeed;

    public float z = -18f;

    void Update()
    {
        followPlayer();
    }

    void followPlayer() {
        Vector3 targetPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, z);
        transform.position = Vector3.Slerp(transform.position, targetPosition, camSpeed);
    }

}
