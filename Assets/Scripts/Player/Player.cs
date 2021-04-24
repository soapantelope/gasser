using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Misc References")]
    public GameObject playerUI;
    public AngryForm angryForm;
    public Button release;
    public SFXManager sfx;
    public float camDistance;

    [Header("Health")]
    public float maxHealth = 100f;
    public float health = 100f;
    public float regenFactor;
    private float regenAmt;

    [Header("Thirst")]
    public float maxThirst = 100f;
    public float thirst = 0f;
    public float thirstRate = 0.1f;
    public float healthDecay;


    [Header("NPC Interaction")]
    public GameObject npc;
    public LayerMask NPCLayer;
    public float talkRadius;
    public bool currentlyTalking = false;

    [Header("Life Suck")]
    public int suckRange = 3;
    public float thirstMultiplier;
    public Tilemap tilemap;
    public Tile deadTile;
    public Tile mombyTile;
    public bool canTransform = false;

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        if (health <= 0)
        {
            die();
        }

        thirst -= thirstRate * Time.deltaTime;
        if (thirst >= 100f)
        {
            thirst = 100;
        }

        else {
            lifeSuck();
            if (thirst >= 97.5 && canTransform)
            {
                release.gameObject.SetActive(true);
            }
            else release.gameObject.SetActive(false);
            if (thirst <= 30) {
                health -= healthDecay * Time.deltaTime;
            }
        }

        NPCInteraction();
        if (thirst <= 100f) lifeSuck();
        regen();
    }

    private void NPCInteraction() {
        if (Input.GetKeyDown(KeyCode.E) && !currentlyTalking && Physics2D.OverlapCircle(transform.position, talkRadius, NPCLayer))
        {
            npc = Physics2D.OverlapCircleAll(transform.position, talkRadius, NPCLayer)[0].gameObject;
            npc.GetComponent<DialogueTrigger>().triggerDialogue();
            currentlyTalking = true;
            gameObject.GetComponent<PlayerMovement>().talking = currentlyTalking;
        }

        else if (currentlyTalking && Input.GetKeyDown(KeyCode.E))
        {
            npc.GetComponent<DialogueTrigger>().nextDialogue();
        }
    }

    public void takeDamage(float damage)
    {
        if (thirst <= 0) thirst = 0;

        health -= damage;
    }

    public void regen() {
        regenAmt = thirst * regenFactor;
        if (health <= 100f)
            health += regenAmt * Time.deltaTime;
    }

    public void die() {
        SceneManager.LoadScene(0);
    }

    public void lifeSuck() {

        int numSucked = 0;
        if (Input.GetKeyDown(KeyCode.S))
        {
            sfx.play("suck");
        }
        else if (Input.GetKeyUp(KeyCode.S)) {
            sfx.stop("suck");
        }

        if (Input.GetKey(KeyCode.S) && thirst <= 100f)
        {
            Vector3Int coord = tilemap.GetComponent<GridLayout>().WorldToCell(transform.position) + new Vector3Int(0, -1, 0);

            for (int i = suckRange * -1; i <= suckRange; i++)
            {
                Vector3Int currentCoord = coord + new Vector3Int(i, 0, 0);
                if (tilemap.HasTile(currentCoord) && tilemap.GetTile(currentCoord) != deadTile && tilemap.GetTile(currentCoord) != mombyTile && !tilemap.HasTile(currentCoord + new Vector3Int(0, 1, 0)))
                {
                    Color color = tilemap.GetColor(currentCoord);
                    tilemap.SetTileFlags(currentCoord, TileFlags.None);
                    tilemap.SetColor(currentCoord, new Color(color.r + 0.4f * Time.deltaTime, color.g + 0.4f * Time.deltaTime, color.b + 0.4f * Time.deltaTime, 1));

                    if (color.g >= 1)
                    {
                        tilemap.SetTile(currentCoord, deadTile);
                    }

                    numSucked++;
                }

            }

            thirst += numSucked * thirstMultiplier * Time.deltaTime;
        }

    }
    public void thirstTransform() {
        sfx.play("roar");
        angryForm.gameObject.SetActive(true);
        angryForm.onCreation();
        angryForm.transform.position = transform.position + new Vector3(0, 4);
        playerUI.SetActive(false);
        gameObject.SetActive(false);
    }

    public void endDialogue(GameObject npc) {
        currentlyTalking = false;
        gameObject.GetComponent<PlayerMovement>().talking = false;
        if (npc.gameObject.name == "Bimby") {
            npc.GetComponent<Bimby>().running = true;
            npc.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (npc.gameObject.name == "Momby") {
            npc.GetComponent<Momby>().startAttacking();
        }
    }
}
