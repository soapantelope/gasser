using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTrigger : MonoBehaviour
{
    public float horiForce;
    public float vertForce;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered");
        if (collision.gameObject.tag == "NPC") {
            collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(horiForce, vertForce), ForceMode2D.Impulse);
        }
    }
}
