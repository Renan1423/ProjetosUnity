using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightController : MonoBehaviour
{
    private Light2D lt;
    public GameObject player;

    Color colorSlowMode = Color.blue;

    void Start()
    {
        lt = GetComponent<Light2D>();
        colorSlowMode = new Color(0.1921569f, 0.2352941f, 0.8980392f);
    }

    void Update()
    {
        //lt.color -= (Color.white / 2.0f) * Time.deltaTime;
        if (player.GetComponent<Player>().life <= 1)
        {
            lt.pointLightInnerRadius = 0;
            lt.pointLightOuterRadius = 18.8f;
        }
    }

    public void SlowModeColorON() {
        lt.color = colorSlowMode;
    }

    public void SlowModeColorOFF()
    {
        lt.color = Color.white;
    }
}
