using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    public float horiMove;
    public float vertMove;
    public float xMax;
    public float xMin;

    private float direction = 1;

    void Update()
    {
        Vector3 pos = transform.position;
        pos += new Vector3(horiMove * direction * Time.deltaTime, vertMove * direction * Time.deltaTime, 0);
        transform.position = pos;

        if (transform.position.x > xMax || transform.position.x < xMin)
        {
            direction *= -1;
        }
    }
}
