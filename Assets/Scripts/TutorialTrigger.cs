using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTrigger : MonoBehaviour
{
    [TextArea(3, 10)]
    public string message;

    public float fadeRate;
    public Text textBox;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player") {
            StartCoroutine(fadeIn());
            textBox.text = message;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player") {
            StartCoroutine(fadeOut());
        }
    }

    IEnumerator fadeIn()
    {
        for (float i = 0; i <= 1; i += fadeRate * Time.deltaTime)
        {
            textBox.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }

    IEnumerator fadeOut()
    {
        for (float i = 1; i >= 0; i -= fadeRate * Time.deltaTime)
        {
            textBox.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }
}
