using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private Player player;
    private CameraController vCam;

    private Shield PampamShield;

    void Start()
    {
        vCam = FindObjectOfType<CameraController>();
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        PampamShield = FindObjectOfType<Shield>(); //Destroy the shield after taking damage
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Trash") {
            if (collision.GetComponent<Trash>().isUnbreakable == false)
            {
                if (player.isShielded == false)
                {
                    player.Lifes--;
                    if (player.comboCount >= 2)
                    {
                        player.Points += player.comboCount;
                    }
                    player.comboCount = 0;
                }
                AudioController.current.PlayMusic(AudioController.current.Damage);
                vCam.CameraShake();
            }
            Destroy(collision.gameObject);
        }    
    }
}
