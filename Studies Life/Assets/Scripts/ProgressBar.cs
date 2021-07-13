using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

    public Data data;

    private Image Progress;

    private float atualProgress = 0;

    private float maxProgress = 13;

    void Start()
    {
        Progress = GetComponent<Image>();
    }


    void Update()
    {
        atualProgress = data.ano;
        Progress.fillAmount = (atualProgress - 5) / (maxProgress - 5);
    }
}
