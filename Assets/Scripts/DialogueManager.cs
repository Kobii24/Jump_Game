using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public Image characterIcon;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueArea;
    // Start is called before the first frame update
    private Queue<DialougeLine> lines;
    public bool isDialogueActive = false;
    public float typingSpeed = 0.2f;
    public Animator animator;
    void Start()
    {
        if(Instance == null)
            Instance = this;
    }

    // Update is called once per frame
    public void StartDialogue(Dialouge dialouge)
    {
        isDialogueActive = true;
        animator.Play("Show");
        lines.Clear();
        foreach(DialougeLine line in dialouge.dialougeLines)
        {
            lines.Enqueue(line);
        } 
        DisplayNextDialougeLine();
    }

    public void DisplayNextDialougeLine()
    {
        if(lines.Count == 0)
        {
            EndDialogue();
            return;
        }
        DialougeLine currentLine = lines.Dequeue();

        characterIcon.sprite = currentLine.character.icon;
        characterName.text = currentLine.character.name;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentLine));
    }

    IEnumerator TypeSentence(DialougeLine dialogueLine)
    {
        dialogueArea.text = "";
        foreach(char letter in dialogueLine.line.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        animator.Play("hide");
    }
}
