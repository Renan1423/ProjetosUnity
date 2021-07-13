using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip main;
    public AudioClip click;
    public AudioClip moveButton;
    public AudioClip moveButtonBack;
    public AudioClip levelUp1;
    public AudioClip levelUp2;
    public AudioClip starSound;
    public AudioClip rest;


    private AudioSource audioSource;
    public Materias materia;

    public static AudioController current;



    void Start()
    {
        current = this;

        audioSource = GetComponent<AudioSource>();
        
    }

    public void PlayMusic(AudioClip clip) {
        audioSource.PlayOneShot(clip);
    }
}
