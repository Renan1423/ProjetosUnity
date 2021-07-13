using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnemy : MonoBehaviour
{

    private GameObject enemy;
    public Image HPBar;

    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");

    }

    void Update()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");

        HPBar.fillAmount = ((enemy.GetComponent<Enemy>().life) / (enemy.GetComponent<Enemy>().maxLife));
    }
}
