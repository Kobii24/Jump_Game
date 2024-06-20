using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueController3 : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI NPCNameText;
    [SerializeField] private TextMeshProUGUI NPCDialogueText;
    [SerializeField] private float typeSpeed = 10;

    private Queue<string> paragraphs = new Queue<string>();

    private bool conversationEnded;
    private bool isTyping;

    private string p;

    private Coroutine typeDialogueCoroutine;
    private const string HTML_ALPHA = "<color=#00000000>";
    private const float MAX_TYPE_TIME = 0.1f;

    public void DisplayNextParagraph(DialogueText dialogueText)
    {
        //if there is nothing in queue
        if (paragraphs.Count == 0)
        {
            if (!conversationEnded)
            {
                //start
                StartConversation(dialogueText);
            }
            else if (conversationEnded && !isTyping)
            {
                //end
                EndConversation();
                return;
            }
        }
        //if there is sth in queue
        if (!isTyping)
        {
            p = paragraphs.Dequeue();

            typeDialogueCoroutine = StartCoroutine(TypeDialogueText(p));
        }
        else
        {
            FinishParagraphEarly();
        }
        //update conversation text
        //NPCDialogueText.text = p;

        //update conversationEnded bool
        if (paragraphs.Count == 0)
        {
            conversationEnded = true;
        }
    }

    private void StartConversation(DialogueText dialogueText)
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        //Update the speaker name
        NPCNameText.text = dialogueText.speakerName;
        //Add dialogue text to the queue
        for (int i = 0; i < dialogueText.paragraphs.Length; i++)
        {
            paragraphs.Enqueue(dialogueText.paragraphs[i]);
        }
    }

    private void EndConversation()
    {
        //clear the queue
        paragraphs.Clear();
        //return bool to false
        conversationEnded = false;

        //deactive gameobject
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }

    //private IEnumerator TypeDialogueText(string p)
    //{
    //    float elapsedTime = 0f;

    //    int charIndex = 0;
    //    charIndex = Mathf.Clamp(charIndex, 0, p.Length);

    //    while(charIndex < p.Length)
    //    {
    //        elapsedTime += Time.deltaTime * typeSpeed;
    //        charIndex = Mathf.FloorToInt(elapsedTime);

    //        NPCDialogueText.text = p.Substring(0, charIndex);

    //        yield return null;

    //    }
    //    NPCDialogueText.text = p;
    //}

    private IEnumerator TypeDialogueText(string p)
    {
        isTyping = true;
        NPCDialogueText.text = "";

        string originalText = p;
        string displayedText = "";
        int alphaIndex = 0;

        foreach (char c in p.ToCharArray())
        {
            alphaIndex++;
            NPCDialogueText.text = originalText;
            displayedText = NPCDialogueText.text.Insert(alphaIndex, HTML_ALPHA);
            NPCDialogueText.text = displayedText;

            yield return new WaitForSeconds(MAX_TYPE_TIME / typeSpeed);
        }
        isTyping = false;
    }

    private void FinishParagraphEarly()
    {
        //stop the coroutine
        StopCoroutine(typeDialogueCoroutine);

        //finish display text
        NPCDialogueText.text = p;

        //update isTyping bool
        isTyping = false;
    }
}
