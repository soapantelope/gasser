using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStopper : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "NPC") {
            collision.gameObject.SetActive(false);
        }
    }
}
