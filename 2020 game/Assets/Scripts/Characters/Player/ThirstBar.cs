using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThirstBar : MonoBehaviour
{
    public float lerpRate = 3f;
    public Player player;
    public Slider slider;

    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        slider.value = player.thirst;
    }

    void Update()
    {
        slider.value = Mathf.Lerp(slider.value, player.thirst, lerpRate * Time.deltaTime);
    }
}
