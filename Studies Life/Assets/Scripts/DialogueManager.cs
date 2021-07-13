using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DialogueType {TUTORIAL, NORMAL}

public class DialogueManager : MonoBehaviour
{

    public DialogueType type;

    public StartGame startGame;

    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    private Queue<string> sentences; // um Queue é como um array, porém mais restrito. É um FIFO (First In, First Out)

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue) {
        animator.SetBool("isOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }

        string sentence = sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence) 
    {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    void EndDialogue() {
        animator.SetBool("isOpen", false);
        if (type == DialogueType.TUTORIAL)
        {
            startGame.NextStage();
        }
        else { 
            
        }
    }
}
