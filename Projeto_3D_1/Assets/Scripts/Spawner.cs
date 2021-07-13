using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float SpawnerTempo;

    public List<GameObject> Paredes = new List<GameObject>();

    float TempoCount;


    void Start()
    {
        
    }

    void Update()
    {
        TempoCount += Time.deltaTime;   
        if (TempoCount >= SpawnerTempo) {
            Instantiate(Paredes[Random.Range(0,Paredes.Count)], transform.position, transform.rotation);
            TempoCount = 0;
        }  

    }
}
