using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public int start; //0 para não, 1 para sim

    void Start()
    {
        if (start == 1)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
        else { 
            
        }
    }

    public void TriggerDialogue() {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
