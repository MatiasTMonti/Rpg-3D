using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public List<string> dialogueLines = new List<string>();
    public string npcName;
    public GameObject dialoguePanel;

    public static DialogueSystem Instance;

    Button continueButton;
    TextMeshProUGUI dialogueText, nameText;
    int dialogueIndex;

    private void Awake()
    {
        continueButton = dialoguePanel.transform.Find("Continue").GetComponent<Button>();
        dialogueText = dialoguePanel.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        nameText = dialoguePanel.transform.Find("Name").GetChild(0).GetComponent<TextMeshProUGUI>();

        continueButton.onClick.AddListener(delegate { ContinueDialogue(); });
        dialoguePanel.SetActive(false);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddNewDialogue(string[] lines, string name)
    {
        dialogueIndex = 0;
        dialogueLines = new List<string>();
        dialogueLines.AddRange(lines);

        this.npcName = name;
        CreateDialogue();
    }

    public void CreateDialogue()
    {
        dialogueText.text = dialogueLines[dialogueIndex];
        nameText.text = npcName;
        dialoguePanel.SetActive(true);
    }

    public void ContinueDialogue()
    {
        if (dialogueIndex < dialogueLines.Count - 1)
        {
            dialogueIndex++;
            dialogueText.text = dialogueLines[dialogueIndex];
        }
        else
        {
            dialoguePanel.SetActive(false);
        }
    }
}
