using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GlobalLight : MonoBehaviour
{
    public GameObject player;
    private Light2D lt;

    void Start()
    {
        lt = GetComponent<Light2D>();
    }

    void Update()
    {
        if (player.GetComponent<Player>().isDead == true)
        {
            lt.intensity = 0.25f;
        }
        else {
            lt.intensity = 1f;
        }
    }
}
