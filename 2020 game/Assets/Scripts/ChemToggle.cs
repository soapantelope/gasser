using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChemToggle : MonoBehaviour
{
    public int chemNumber;
    public PlayerFight player;
    public Image image;

    public void Start()
    {
        image = gameObject.GetComponent<Image>();
    }

    void Update()
    {
        if (player.currentChem == chemNumber)
        {
            image.color = Color.green;
        }
        else image.color = Color.white;
    }
}
