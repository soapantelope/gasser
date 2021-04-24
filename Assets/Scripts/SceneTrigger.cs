using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class SceneTrigger : MonoBehaviour
{
    public CameraFollow cam;
    public SceneTrigger targetPlace;
    public ExitState exitState;
    public Image black;

    private bool playerEntered = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!exitState.inTrigger && col.gameObject.tag == "Player") { 
            {
                playerEntered = true;
                black.gameObject.GetComponent<SceneFade>().StartCoroutine("fade");
                exitState.inTrigger = true;
                StartCoroutine(waitForTransition(col));
            }
        }

        if (col.gameObject.tag == "NPC" && gameObject.name != "Room 4 Enter")
        {
            col.gameObject.transform.SetParent(targetPlace.gameObject.transform.parent);
            col.gameObject.transform.SetAsFirstSibling();
            col.transform.position = targetPlace.transform.position;
        }

    }    

    private void OnTriggerExit2D(Collider2D col)
    {
        if (!playerEntered && exitState.inTrigger && col.gameObject.tag == "Player")
        {
            exitState.inTrigger = false;
        }
        else if (playerEntered && col.gameObject.tag == "Player") {
            playerEntered = false;
        }
    }

    IEnumerator waitForTransition(Collider2D col) {
        yield return new WaitForSeconds(0.5f);        
        transform.parent.gameObject.SetActive(false);
        GameObject targetScene = targetPlace.transform.parent.gameObject;
        cam.setBounds(targetScene.GetComponent<Room>());
        targetScene.SetActive(true);
        col.transform.position = targetPlace.GetComponent<SpriteRenderer>().bounds.center;
        col.gameObject.GetComponent<Player>().tilemap = targetScene.transform.GetChild(targetScene.transform.childCount - 1).GetChild(0).GetComponent<Tilemap>();
    }
}
