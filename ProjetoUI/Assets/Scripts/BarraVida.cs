using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public float Vida;
    public int MaxVida = 100;
    public Image Barra;

    void Start()
    {
        
    }

    void Update()
    {

        Barra.fillAmount = Vida / MaxVida;

    }
}
