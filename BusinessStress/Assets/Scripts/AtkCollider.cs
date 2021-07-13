using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkCollider : MonoBehaviour
{
    public LayerMask collisionLayer;
    public float radius = 1f;
    public float damage = 1f;

    public bool is_Player, is_Enemy,is_Object;

    public GameObject hit_FX;

    private GameObject VCam;

    private GameObject PointsAnim;


    void Start()
    {
        VCam = GameObject.FindGameObjectWithTag("MainCamera");
        VCam.GetComponent<CameraController>();

        PointsAnim = GameObject.FindGameObjectWithTag("Points");

    }

    void Update()
    {
        DetectCollision();
    }

    void DetectCollision()
    {
        
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, collisionLayer);

        bool Targeting = hit;

        if (Targeting == true) {
            if (is_Player)
            {
                hit.gameObject.GetComponent<Enemy>().isTakingDamage = true;
                hit.gameObject.GetComponent<Object>().isTakingDamage = true;
                Player.Points += 1;
                PointsAnim.GetComponent<Animator>().SetTrigger("Increase");

                if (Player.isHeavyAttacking == false)
                {
                    VCam.GetComponent<CameraController>().CameraSmallShake();
                }
                else
                {
                    AudioController.current.PlayMusic(AudioController.current.punchHeavy2);
                    VCam.GetComponent<CameraController>().CameraLongShake();
                }


            }
            else if (is_Enemy)
            {
                hit.gameObject.GetComponent<Player>().isTakingDamage = true;
                Player.Points -= 1;
                PointsAnim.GetComponent<Animator>().SetTrigger("Increase");
                VCam.GetComponent<CameraController>().CameraMediumShake();
            }

            else if (is_Object) {
                hit.gameObject.GetComponent<Enemy>().isTakingDamage = true;
                Player.Points += 200;
                PointsAnim.GetComponent<Animator>().SetTrigger("Increase");
                AudioController.current.PlayMusic(AudioController.current.punchHeavy2);
                VCam.GetComponent<CameraController>().CameraMediumShake();
                Destroy(hit.gameObject);
            }
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
