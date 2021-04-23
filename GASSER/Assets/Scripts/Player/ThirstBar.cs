using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThirstBar : MonoBehaviour
{
    public float lerpRate = 3f;
    public Player player;
    public Slider slider;
    public Image bar;

    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        slider.value = player.thirst;
        bar = slider.fillRect.gameObject.GetComponent<Image>();
    }

    void Update()
    {
        slider.value = Mathf.Lerp(slider.value, player.thirst, lerpRate * Time.deltaTime);
        if (player.thirst <= 30)
        {
            bar.color = Color.red;
        }
        else bar.color = Color.black;
    }
}
