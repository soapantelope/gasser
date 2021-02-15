using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class SceneTrigger : MonoBehaviour
{
    public SceneTrigger targetPlace;
    public ExitState exitState;

    private bool transitioned = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!exitState.inTrigger && col.gameObject.tag == "Player")
        {
            exitState.inTrigger = true;
            transitioned = true;
            transform.parent.gameObject.SetActive(false);
            GameObject targetScene = targetPlace.transform.parent.gameObject;
            targetScene.SetActive(true);
            col.transform.position = targetPlace.transform.position;
            col.gameObject.GetComponent<Player>().tilemap = targetScene.transform.GetChild(targetScene.transform.childCount - 1).GetChild(0).GetComponent<Tilemap>();
        }

        transitioned = false;
    }    
    private void OnTriggerExit2D(Collider2D col)
    {
        if (!transitioned && col.gameObject.tag == "Player") exitState.inTrigger = false;
    }
}
