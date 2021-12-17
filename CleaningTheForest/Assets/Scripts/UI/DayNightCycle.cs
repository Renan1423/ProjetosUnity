using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{
    /*private Light2D Light;

    [SerializeField] private Light2D[] TimeColors;

    private Light2D UpdateColor;

    private int time;

    private Animator anim;
    private bool isHappening;*/

    void Start()
    {
        /*
        Light = GetComponent<Light2D>();

        anim = GetComponent<Animator>();

        time = Random.Range(0, TimeColors.Length);

        Light.color = TimeColors[time].color;
        UpdateColor = Light;*/
    }

    // Update is called once per frame
    void Update()
    {
        //Light.color = UpdateColor.color;
        /*if (isHappening == false)
        {
            isHappening = true;
            StartCoroutine(TimeCycle());
        }*/
    }

    /*IEnumerator TimeCycle()
    {

        yield return new WaitForSeconds(10f);

        anim.SetTrigger("DayCycle");

        yield return new WaitForSeconds(10f);

        isHappening = false;
    }*/
}
