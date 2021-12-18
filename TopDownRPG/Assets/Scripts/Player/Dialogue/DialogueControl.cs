using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{
    [System.Serializable]
    public enum idiom { 
        pt,
        eng,
        spa
    }

    public idiom language;

    [Header("Components")]
    public GameObject dialogueObj; //dialog window
    public Image profileSprite; //profile sprite
    public Text speechText;
    public Text actorNameText; //NPC's name

    [Header("Settings")]
    public float typingSpeed; //speech speed

    //Control variables
    private bool isShowing; //check if the window is visible
    private int index; //sentences index
    private string[] sentences;

    public static DialogueControl instance;

    private void Awake() //is called before the Start()
    {
        instance = this;
    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    IEnumerator TypeSentence() {
        foreach (char letter in sentences[index].ToCharArray()) 
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);

        }
    }

    //Call the next sentence
    public void NextSentence() {
        if (speechText.text == sentences[index]) {
            if (index < sentences.Length - 1)
            {
                index++;
                speechText.text = "";
                StartCoroutine(TypeSentence());
            }
            else { // no text left

                speechText.text = "";
                index = 0;
                dialogueObj.SetActive(false);
                sentences = null;
                isShowing = false;
            }
        }
    }


    //Show the window and start the speech
    public void Speech(string[] txt) {
        if (!isShowing)
        {
            dialogueObj.SetActive(true);
            sentences = txt;
            StartCoroutine(TypeSentence());
            isShowing = true;
        }
    }

}
