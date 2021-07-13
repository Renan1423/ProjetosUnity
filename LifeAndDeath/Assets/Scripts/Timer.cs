using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    private float mil;
    private int seg;
    private int min;
    private int timeFinal;
    private int timeFinalCount;
    private int finalPoints;
    public Text timerText;
    public GameObject Player;
    public Text timeResult;
    public Text pointResult;

    private bool EndResults;

    void Start()
    {
        seg = 0;
        min = 0;
        timeFinal = 0;
        timeFinalCount = 0;
    }

    void FixedUpdate()
    {
        if (Player.GetComponent<Player>().StageEnding == false)
        {
            TimerCount();
        }
        else {
            timeFinal = (min*60);
            if (seg != 0) {
                timeFinal += seg;
            }
            if (!EndResults)
            {
                GettingTimerPoints();
                GettingPoints();
            }
        }
    }

    void TimerCount() {
        mil += Time.deltaTime;
        if (mil >= 1)
        {
            mil = 0;
            seg++;
            if (seg > 59)
            {
                seg = 0;
                min++;
            }
            if (seg < 10)
            {
                timerText.text = "" + min + ":0" + seg;
            }
            else {
                timerText.text = "" + min + ":" + seg;
            }
        }
    }

    void GettingTimerPoints() {

        if (timeFinalCount < timeFinal)
        {
            timeFinalCount++;
            timeResult.text = "" + timeFinalCount;
        }
        else {
            timeResult.text = "" + timeFinalCount;
        }
    }

    void GettingPoints() {
        if (finalPoints < Player.GetComponent<Player>().points)
        {
            finalPoints+=4;
            pointResult.text = "" + finalPoints;
        }
        else
        {
            if (finalPoints > Player.GetComponent<Player>().points) {
                finalPoints = Player.GetComponent<Player>().points;
            }
            pointResult.text = "" + finalPoints;
            StartCoroutine("GettingFinalResult");
        }
        
    }

    IEnumerator GettingFinalResult() {
        yield return new WaitForSeconds(1.25f);

        finalPoints -= timeFinalCount;
        timeFinalCount = 0;
        timeResult.text = "0";
        pointResult.text = "" + finalPoints;
        EndResults = true;
    }
}
