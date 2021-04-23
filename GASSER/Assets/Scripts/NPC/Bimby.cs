using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bimby : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public bool running = false;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (running && rb.velocity.magnitude <= speed) rb.velocity += new Vector2(1, 0);
    }

}
