using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialougeCharacter
{
    public string name;
    public Sprite icon;
}

[System.Serializable]
public class DialougeLine
{
    public DialougeCharacter character;
    [TextArea(3, 10)]
    public string line;
}

[System.Serializable]
public class Dialouge
{
    public List<DialougeLine> dialougeLines = new List<DialougeLine>();
}

public class DialougeTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public Dialouge dialouge;
    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialouge);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            TriggerDialogue();
    }
}
