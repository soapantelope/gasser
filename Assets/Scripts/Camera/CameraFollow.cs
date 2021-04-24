using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;

    public Room firstRoom;
    public Transform player;
    public Vector3 offset;
    public float currentCamSpeed;
    public float angryCamSpeed;
    public float normalCamSpeed;

    public float z = -18f;


    private void Start()
    {
        setBounds(firstRoom);
    }

    void Update()
    {
        followPlayer();
    }

    void followPlayer()
    {
        Vector3 targetPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, z);
        targetPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, xMin, xMax),
            Mathf.Clamp(targetPosition.y, yMin, yMax),
            z);
        transform.position = Vector3.Slerp(transform.position, targetPosition, currentCamSpeed);
    }

    public void setBounds(Room room) {
        float[] setBounds = room.bounds;
        xMin = setBounds[0];
        xMax = setBounds[1];
        yMin = setBounds[2];
        yMax = setBounds[3];
        if (room.z != 0f)
            z = room.z;
        else z = -18f;
    }
}