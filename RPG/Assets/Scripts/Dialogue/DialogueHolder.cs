using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour
{
    public string dialogue;
    private DialogueManager dManager;

    public string[] dialogueLines;

    private void Start()
    {
        dManager = FindObjectOfType<DialogueManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (!dManager.dialogueActive)
                {
                    dManager.dialogueLines = dialogueLines;
                    dManager.currentLines = 0;
                    dManager.ShowDialogue();
                }
            }
        }
    }
}
