using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneFade : MonoBehaviour
{
    public float fadeRate;

    private Image img;


    private void Start()
    {
        img = gameObject.GetComponent<Image>();
    }

    IEnumerator fade()
    {
        for (float i = 0; i <= 1; i += fadeRate * Time.deltaTime)
        {
            img.color = new Color(0, 0, 0, i);
            yield return null;
        }

        for (float i = 1; i >= 0; i -= fadeRate * Time.deltaTime)
        {
            img.color = new Color(0, 0, 0, i);
            yield return null;
        }
    }
}
