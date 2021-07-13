using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip die;
    public AudioClip doorClose;
    public AudioClip jump;
    public AudioClip hitHurt;
    public AudioClip getItem;

    private AudioSource audioSource;

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
