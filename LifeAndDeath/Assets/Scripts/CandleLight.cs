using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CandleLight : MonoBehaviour
{

    public GameObject player;
    private Light2D lt;

    void Start()
    {
        lt = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Player>().isDead == true)
        {
            lt.intensity = 1f;
        }
        else
        {
            lt.intensity = 0f;
        }
    }
}
