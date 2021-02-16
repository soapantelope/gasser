using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject textBox;
    public Text nameText;
    public Text dialogueText;

    private Queue<string> sentences;
    public Player player;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void startDialogue(Dialogue dialogue) {

        textBox.SetActive(true);
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        displayNextSentence();
    }

    public void displayNextSentence() {
        if (sentences.Count <= 0) {
            endDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    private void endDialogue() {
        player.currentlyTalking = false;
        player.gameObject.GetComponent<PlayerMovement>().talking = false;
        textBox.SetActive(false);
    }
}
