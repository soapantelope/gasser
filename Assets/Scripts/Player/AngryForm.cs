using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
public class AngryForm : MonoBehaviour
{
    public Slider timerBar;
    public Player player;
    public Hand hand;

    public float lifespan;
    public float camDistance;

    private float startTime;
    private float currentTime;

    // Update is called once per frame
    void Update()
    {
        float percentTime = (Time.time - startTime) / lifespan;
        timerBar.value = 1 - (Time.time - startTime) / lifespan;
        if (percentTime >= 1) {
            player.transform.position = transform.position;
            player.gameObject.SetActive(true);
            player.playerUI.SetActive(true);
            timerBar.gameObject.SetActive(false);
            hand.gameObject.SetActive(false);
            hand.cam.m_Lens.FieldOfView = player.camDistance;
            hand.cam.Follow = player.transform;
            player.thirst = 0;
            gameObject.SetActive(false);
        }
    }

    public void onCreation() {
        timerBar.gameObject.SetActive(true);
        hand.gameObject.SetActive(true);
        hand.cam.Follow = hand.transform;
        hand.cam.m_Lens.FieldOfView = camDistance;
        startTime = Time.time;
        // Do things on creation
    }
}
