using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wraith_21 : NPC_21, ITalkable
{
    [SerializeField] private DialogueText dialogueText;
    [SerializeField] private DialogueController dialogueController;
    public override void Interact()
    {
        Talk(dialogueText);
    }

    public void Talk(DialogueText dialogueText)
    {
        //start conversation
        dialogueController.DisplayNextParagraph(dialogueText);
    }
}
