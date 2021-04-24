using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AngryForm : MonoBehaviour
{
    public Slider timerBar;
    public Player player;
    public Hand hand;
    public TutorialTrigger handMessage;

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
            hand.cam.z = player.camDistance;
            hand.cam.currentCamSpeed = hand.cam.normalCamSpeed;
            hand.cam.player = player.transform;
            player.thirst = 30;
            gameObject.SetActive(false);
        }
    }

    public void onCreation() {
        timerBar.gameObject.SetActive(true);
        hand.gameObject.SetActive(true);
        handMessage.gameObject.SetActive(true);
        hand.cam.player = hand.transform;
        hand.cam.currentCamSpeed = hand.cam.angryCamSpeed;
        hand.cam.z = camDistance;
        startTime = Time.time;
        // Do things on creation
    }
}
