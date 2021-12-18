using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip evilLaughSFX;
    public AudioClip forcePulseSFX;
    public AudioClip spellSFX;
    public AudioClip teleportSFX;
    public AudioClip arrowSFX;
    public AudioClip enemyDeadSFX;
    public AudioClip explosionSFX;
    public AudioClip buttonSoundSFX;
    public AudioClip bgm;

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
