using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathSymbol : MonoBehaviour
{
    private SpriteRenderer spriterenderer;
    private Rigidbody2D rig;

    private int Number;
    private float grav;

    public Sprite Mais;
    public Sprite Menos;
    public Sprite Vezes;
    public Sprite Dividir;

    void Start()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
        rig = GetComponent<Rigidbody2D>();

        Number = Random.Range(0,5);

        grav = Random.Range(0.1f, 1.1f);
        rig.gravityScale = grav;
        

        if (Number == 1)
        {
            spriterenderer.sprite = Mais;
        }
        else if (Number == 2) 
        {
            spriterenderer.sprite = Menos;
        }
        else if (Number == 3)
        {
            spriterenderer.sprite = Vezes;
        }
        else if (Number == 4)
        {
            spriterenderer.sprite = Dividir;
        }
    }
}
