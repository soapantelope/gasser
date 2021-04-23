using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float camSpeed;

    public float z;

    private void Start()
    {
        z = player.gameObject.GetComponent<Player>().camDistance;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
    }

}
