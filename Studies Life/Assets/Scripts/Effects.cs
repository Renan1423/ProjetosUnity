using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    public float Speed;
    public float Life;

    void Start()
    {
        Destroy(gameObject, Life);
    }


    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * Speed);
    }
}
