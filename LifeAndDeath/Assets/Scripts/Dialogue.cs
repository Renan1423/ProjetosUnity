using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public string[] speechTxt;
    public string actorName;

    private DialogueControl dc;

    void Start()
    {
        dc = FindObjectOfType<DialogueControl>();
    }

    void Update()
    {
        dc.Speech(speechTxt,actorName);

        if (Input.GetKeyDown(KeyCode.Return)){
            dc.NextSentence();
        }
    }
}
