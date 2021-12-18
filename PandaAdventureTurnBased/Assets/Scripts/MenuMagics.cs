using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuMagics : MonoBehaviour
{
    public int magicNumber = 0;

    public Text MagicName;
    public Image MagicIcon;

    public GameObject Player;
    private Magics PlayerMagics;

    public Transform MagicSelector;
    public GameObject MagicPanel;
    private Transform trans;

    private BattleSystem battleSystem;

    void Start()
    {
        MagicSelector = FindObjectOfType<MagicSystem>().transform;

        trans = GetComponent<Transform>();
        trans.SetParent(MagicSelector);
        trans.localScale = new Vector3(1,1,1);
        trans.localPosition = new Vector3(0f, 80f - ((magicNumber) * 40), 0f);

        PlayerMagics = Player.GetComponent<Magics>();

        MagicName.text = PlayerMagics.magics[magicNumber];
        MagicIcon.sprite = PlayerMagics.magicsIcons[magicNumber];

        battleSystem = FindObjectOfType<BattleSystem>();
    }

    public void OnSelected()
    {
        StartCoroutine(battleSystem.PlayerMagic(magicNumber, PlayerMagics.magics[magicNumber]));
    }
}
