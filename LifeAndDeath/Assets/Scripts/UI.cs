using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{

    public GameObject player;
    private Image sprite;
    public Sprite aliveSprite;
    public Sprite deadSprite;

    public Text condicao;
    public Text pontos;

    void Start()
    {
        sprite = GetComponent<Image>();
    }

    void ChangeSprite()
    {
        if (player.GetComponent<Player>().isDead == true)
        {
            sprite.sprite = deadSprite;
            condicao.text = "DEAD";
        }
        else {
            sprite.sprite = aliveSprite;
            condicao.text = "ALIVE";
        }
    }
    void Update()
    {
       ChangeSprite();
       pontos.text = "POINTS:" + player.GetComponent<Player>().points;
        if (player.GetComponent<Player>().points > 1000)
        {
            pontos.color = Color.green;
        }
        else if (player.GetComponent<Player>().points < 1000)
        {
            pontos.color = Color.red;
        }
        else if (player.GetComponent<Player>().points == 1000) {
            pontos.color = Color.white;
        }
    }

    
}
