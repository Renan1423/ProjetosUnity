using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    public float Speed;
    public float Life;

    public bool UI;

    void Start()
    {
        
        
    }

    IEnumerator SetFalse() {
        yield return new WaitForSeconds(Life);

        gameObject.SetActive(false);
    }

    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * Speed);
        if (UI)
        {
            StartCoroutine(SetFalse());
        }
        else
        {
            Destroy(gameObject, Life);
        }
    }
}
