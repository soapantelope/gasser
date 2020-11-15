using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float camSpeed;

    public float mapX, mapY;
    private float left, right, bottom, top;

    void Start()
    {
        // How much the camera sees vertically in world space
        float YExtent = Camera.main.orthographicSize;
        // How much the camera sees horizontally (using X : Y ratio) in world space
        float XExtent = YExtent * (Screen.width / Screen.height);

        print(XExtent + ", " + YExtent);

        // DEBUG LATER LOL THIS DOESNT WORK ASKFJSDGSODIGJRSGKJRSOIGNRSI
        // Note: Only works if map & camera are positioned at origin!
        left = XExtent - mapX / 2.0f;
        right = mapX / 2.0f - XExtent;
        bottom = YExtent - mapY / 2.0f;
        top = mapY / 2.0f - YExtent;

        print(left + ", " + right + ", " + bottom + ", " + top);
    }

    void Update()
    {
        Vector3 targetPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z);
        transform.position = Vector3.Slerp(transform.position, targetPosition, camSpeed);

        // Camera bounds
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, left, right);
        pos.y = Mathf.Clamp(pos.y, bottom, top);
        transform.position = pos;
    }
}
