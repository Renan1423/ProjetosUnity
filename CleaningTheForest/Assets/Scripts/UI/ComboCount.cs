using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboCount : MonoBehaviour
{
    private Player player;

    public Text comboText;
    public Text comboShadowText;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        comboText.text = "" + player.comboCount;
        comboShadowText.text = "" + player.comboCount;

    }
}
