using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public CameraFollow cam;
    public Rigidbody2D rb;
    public float speed;

    private bool touching = false;
    private GameObject subject;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = followMouse();
        if (Input.GetMouseButton(0) && touching)
        {
            subject.GetComponent<Rigidbody2D>().velocity = followMouse();
        }

    }

    private Vector2 followMouse() {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -cam.transform.position.z;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        return (mousePos - transform.position) * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Boss") {
            subject = collision.gameObject;
            touching = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Boss") {
            touching = false;
        }
    }
}
