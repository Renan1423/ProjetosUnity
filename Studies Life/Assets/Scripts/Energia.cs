using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energia : MonoBehaviour
{

    public Image energyBar;
    public float maxEnergyValue;
    public float energyValue;
    public float energyCost;


    void Update()
    {
        energyBar.fillAmount = energyValue / maxEnergyValue;
    }
}
