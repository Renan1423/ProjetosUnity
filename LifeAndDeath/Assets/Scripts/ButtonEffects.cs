using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEffects : MonoBehaviour
{

    private Transform trans;

    public Vector2 scaleChange;

    void Start()
    {
        trans = GetComponent<Transform>();

        scaleChange = new Vector2(0.6f, 0.6f);
    }

    public void OnOver() {
        trans.localScale = scaleChange;
    }

    public void NotOnOver() {
        trans.localScale = new Vector2(0.45714f, 0.45714f);
    }
}
