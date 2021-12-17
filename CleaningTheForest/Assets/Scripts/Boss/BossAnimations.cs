using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimations : MonoBehaviour
{

    private CameraController vCam;
    private Spawner spawner;

    void Start()
    {
        vCam = FindObjectOfType<CameraController>();
        vCam.TriggerBoss(); //Start camera's animation
        vCam.BossCameraShake();

        spawner = FindObjectOfType<Spawner>();
        spawner.enabled = false; //Stopping the spawner


        AudioController.current.PlayBossBGM(); //starting the boss bgm
    }
}
