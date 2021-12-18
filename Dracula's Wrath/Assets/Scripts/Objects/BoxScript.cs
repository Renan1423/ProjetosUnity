using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    public float life;

    public Animator GFX;

    public GameObject MPSpherePrefab;

    public IEnumerator TakeDamage(float dmg)
    {
        if (life > 0)
        {
            life -= dmg;

            GFX.SetInteger("Transition", 1);

            yield return new WaitForSeconds(0.3f);

            GFX.SetInteger("Transition", 0);
            
            if (life <= 0)
            {
                int createMana = Random.Range(0, 100);
                if (createMana < 55)
                {
                    Instantiate(MPSpherePrefab, transform);
                }
                AudioController.current.PlayMusic(AudioController.current.explosionSFX);

                Destroy(gameObject,0.1f);
            }
        }
    }
}
