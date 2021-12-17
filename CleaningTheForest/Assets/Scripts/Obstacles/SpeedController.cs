using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    [Header("Speed Controller")]

    public Player player;
    public float objSpeed;

    public float maxSpeed;

    public float initialSpeed = 1;

    void Awake()
    {
        objSpeed = initialSpeed;
    }

    void Update()
    {
        
        if (player.Points >= 50)
        {
            objSpeed = initialSpeed + (0.1f * (player.Points / 100));
            if (objSpeed >= maxSpeed)
            {
                objSpeed = maxSpeed;
            }
        }
        else
        {
            objSpeed = initialSpeed;
        }
    }
}
