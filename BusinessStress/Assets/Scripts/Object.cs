using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public float life;
    public bool isThrowed;
    private Rigidbody2D rig;
    private Transform trans;

    public float dmgTime = 0.1f;
    public float dmgMaxTime = 0.1f;

    public bool isTakingDamage;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
    }

    void Update()
    {
        if (life > 0) {
            StartCoroutine(OnDamage());
        }
    }

    public IEnumerator OnDamage()
    {
        if (dmgTime < dmgMaxTime)
        {
            dmgTime += Time.deltaTime;
        }

        if (isTakingDamage)
        {

            if (dmgTime >= dmgMaxTime)
            {
                life--;
                int n = Random.Range(0, 10);
                AudioController.current.PlayMusic(AudioController.current.punchHeavy);
                dmgTime = 0;
            }

            yield return new WaitForSeconds(0.3f);

            isTakingDamage = false;

        }
    }

    public void DestroyThisGameObject() {
        Destroy(gameObject, 0.5f);
    }
}
