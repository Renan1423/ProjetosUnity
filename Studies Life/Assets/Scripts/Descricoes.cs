using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Descricoes : MonoBehaviour
{

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();    
    }

    public void ButtonSelected()
    {
        StartCoroutine(ButtonSelecSound());
        anim.SetBool("Over", true);
    }

    IEnumerator ButtonSelecSound()
    {
        yield return new WaitForSeconds(0.9f);

        AudioController.current.PlayMusic(AudioController.current.moveButton);
    }

    public void ButtonNotSelected()
    {
        StartCoroutine(ButtonNotSelecSound());
        anim.SetBool("Over", false);
    }

    IEnumerator ButtonNotSelecSound()
    {
        yield return new WaitForSeconds(0.9f);

        AudioController.current.PlayMusic(AudioController.current.moveButtonBack);
    }
}
