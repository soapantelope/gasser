using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTrigger : MonoBehaviour
{
    public string targetScene;
    public string enterOrExit = "Enter";

    public ExitState state;

    private void Update()
    {
        Debug.Log(state.exit);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Entered new trigger");
        if (state.exit && col.gameObject.tag == "Player")
        { 
            Static.enterExit = enterOrExit;
            SceneManager.LoadScene(targetScene);
        }
    }    
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player") state.exit = true;
        Debug.Log("Exit the entrance");
    }
}
