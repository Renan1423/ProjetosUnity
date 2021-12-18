using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGame : MonoBehaviour
{
    private Transform trans;

    private Player player;

    void Start()
    {
        trans = GetComponent<Transform>();

        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        trans.position = player.transform.position;   
    }
}
