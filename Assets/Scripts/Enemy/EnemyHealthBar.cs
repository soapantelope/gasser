using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Change this later to have inheritance for health bars
public class EnemyHealthBar : MonoBehaviour
{
    public float lerpRate = 3f;
    public Enemy enemy;
    public Slider slider;

    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        slider.value = enemy.health;
    }

    void Update()
    {
        slider.value = Mathf.Lerp(slider.value, enemy.health, lerpRate * Time.deltaTime);
    }
}
