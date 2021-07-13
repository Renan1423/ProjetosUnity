using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{

    public Sprite[] sprites;
    private SpriteRenderer spr;

    private Transform trans;
    
    public float Speed;
    public float Life;

    public float minSize;

    public float maxSize;


    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        trans = GetComponent<Transform>();

        spr.sprite = sprites[Random.Range(0,sprites.Length)];

        float newScale = Random.Range(minSize , maxSize);
        trans.localScale = new Vector3(newScale,newScale,newScale);

        Destroy(gameObject, Life);

        Speed = Random.Range(0.1f, 1.5f);
        
    }

    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * Speed);
        
    }
}
