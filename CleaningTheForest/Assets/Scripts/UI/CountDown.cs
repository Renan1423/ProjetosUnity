using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDown : MonoBehaviour
{

    public Spawner spawner;
    public PowerUpSpawner powerUpSpawner;
    public Player player;

    void Awake()
    {
        StartCoroutine(countDown());    
    }

    IEnumerator countDown() {

        yield return new WaitForSeconds(4f);

        spawner.Started = true;
        powerUpSpawner.Started = true;
        player.Started = true;
        Destroy(gameObject);

    }
}
