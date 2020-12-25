using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Player player;
    public Slider slider;

    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        slider.value = player.health;
    }

    void Update()
    {
        slider.value = player.health;
    }
}
