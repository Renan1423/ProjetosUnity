using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip Menu;
    public AudioClip BattleMusic;

    public AudioClip punch;
    public AudioClip punchHeavy;
    public AudioClip punchHeavy2;

    private AudioSource audioSource;

    public static AudioController current;

    void Start()
    {
        current = this;

        audioSource = GetComponent<AudioSource>();

    }

    public void PlayMusic(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void ChangeBGM() {
        audioSource.clip = BattleMusic;
    }
    public void ChangeBGMTitle()
    {
        audioSource.clip = Menu;
    }
}
