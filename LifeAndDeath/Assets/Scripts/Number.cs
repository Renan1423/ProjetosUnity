using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Number : MonoBehaviour
{
    public float Speed;

    void Start()
    {
        Destroy(gameObject, 1f);
    }


    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * Speed);
    }
}
