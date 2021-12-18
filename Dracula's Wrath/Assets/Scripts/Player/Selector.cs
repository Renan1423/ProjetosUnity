using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selector : MonoBehaviour
{
    private Player Player;

    public Image[] magics;

    void Start()
    {
        Player = FindObjectOfType<Player>();
    }

    void Update()
    {
        transform.position = magics[Player.magicSelection].transform.position;
    }
}
