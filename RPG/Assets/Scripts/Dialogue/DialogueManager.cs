using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject dBox;
    public TextMeshProUGUI dText;

    public bool dialogueActive;

    public string[] dialogueLines;
    public int currentLines;

    private void Update()
    {
        if (dialogueActive && Input.GetKeyDown(KeyCode.Space)) 
        {

            currentLines++;
        }

        if (currentLines >= dialogueLines.Length)
        {
            dBox.SetActive(false);
            dialogueActive = false;

            currentLines = 0;
        }

        dText.text = dialogueLines[currentLines];
    }

    public void ShowBox(string dialogue)
    {
        dialogueActive = true;
        dBox.SetActive(true);
        dText.text = dialogue;
    }

    public void ShowDialogue()
    {
        dialogueActive = true;
        dBox.SetActive(true);
    }
}
