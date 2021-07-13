using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpImage : MonoBehaviour
{
    private Image sprite;
    public Sprite[] listaSprites;

    public int SelectPowerUp;

    void Start()
    {
        sprite = GetComponent<Image>();
    }

    void Update()
    {
        sprite.sprite = listaSprites[SelectPowerUp];
    }
}
