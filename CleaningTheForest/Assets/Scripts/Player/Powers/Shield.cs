using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float durability;

    private Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();

        player.isShielded = true;

        transform.SetParent(player.GetComponent<Transform>());
        
        Destroy(gameObject, durability);

        StartCoroutine(DestroyShield());
    }

    IEnumerator DestroyShield() {
        yield return new WaitForSeconds(durability - 0.05f);

        player.isShielded = false;
    }
}
