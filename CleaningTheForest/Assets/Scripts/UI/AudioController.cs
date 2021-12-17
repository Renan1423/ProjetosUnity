using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip DestroyObstacle;
    public AudioClip Damage;
    public AudioClip Click;

    public AudioClip bgm1;
    public AudioClip bgm2;
    public AudioClip bgm3;

    public AudioClip bossBgm;

    private AudioSource audioSource;

    public static AudioController current;

    private bool selectedMusic = false;

    void Start()
    {

        current = this;

        audioSource = GetComponent<AudioSource>();

        ChangeBGM();
    }

    public void PlayMusic(AudioClip clip) {
        audioSource.PlayOneShot(clip);
    }

    public void ChangeBGM() {
        if (!selectedMusic)
        {
            int music = Random.Range(1, 4);

            if (music == 1)
            {
                audioSource.clip = bgm1;
            }
            else if (music == 2)
            {
                audioSource.clip = bgm2;
            }
            else if (music == 3)
            {
                audioSource.clip = bgm3;
            }

            audioSource.Play();

            selectedMusic = true;
        }
    }

    public void PlayBossBGM()
    {
        audioSource.Stop();

        audioSource.clip = bossBgm;

        audioSource.Play();
    }
}
