using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float life;

    void Start()
    {
        Destroy(gameObject, life);
    }

    void Update()
    {
        
    }
}
