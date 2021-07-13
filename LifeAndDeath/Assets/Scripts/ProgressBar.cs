using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private GameObject Player;
    private GameObject Key;

    private Slider thisSlider;

    void Start()
    {
        thisSlider = GetComponent<Slider>();

        Player = GameObject.FindGameObjectWithTag("Player");
        Key = GameObject.FindGameObjectWithTag("Key");
    }

    void Update()
    {
        thisSlider.value = 10/ ((Vector3.Distance(Player.transform.position, Key.transform.position))); 
    }
}
