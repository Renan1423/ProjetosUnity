using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimateController : MonoBehaviour
{
    public GameObject Rain;

    public float rainingPercentage;
    public float count; // starts raining when reachs the limit
    public float countLimit;

    private bool isRaining;

    private void Update()
    {
        count += Time.deltaTime;

        if (count >= countLimit) {
            count = 0;
            StartRaining();
        }
    }

    public void StartRaining() {


        int ran = Random.Range(0, 100);

        if (ran <= rainingPercentage && !isRaining)
        {
            isRaining = true;
            Rain.SetActive(true);
        }
        else {
            isRaining = false;
            Rain.SetActive(false);
        }
    }
}
