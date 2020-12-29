using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float camSpeed;

    // This creates a line of triggers where the camera scale changes
    public List<int> xTriggerPoints = new List<int>();
    public List<float> triggerScales = new List<float>();

    public float mapX, mapY;
    private float left, right, bottom, top;
    private float z;

    void Start()
    {
        setCameraBounds();
    }

    void Update()
    {
        followPlayer();
        clampToBounds();
    }

    void followPlayer() {
        Vector3 targetPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, z);
        transform.position = Vector3.Slerp(transform.position, targetPosition, camSpeed);
    }

    void setCameraBounds() {
        float YExtent = Camera.main.orthographicSize;
        float XExtent = YExtent * (Screen.width / Screen.height);
        z = transform.position.z;

        // Map & camera have to be at origin
        left = XExtent - mapX / 2.0f;
        right = mapX / 2.0f - XExtent;
        bottom = YExtent - mapY / 2.0f;
        top = mapY / 2.0f - YExtent;
    }

    void clampToBounds() {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, left, right);
        pos.y = Mathf.Clamp(pos.y, bottom, top);
        transform.position = pos;
    }
}
