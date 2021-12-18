using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSystem : MonoBehaviour
{
    public GameObject Player;
    private Magics PlayerMagics;

    public GameObject MagicPrefab;
    private Transform trans;
    private bool MagicsInstantiated = false;

    void Start()
    {
        trans = GetComponent<Transform>();

        PlayerMagics = Player.GetComponent<Magics>();

        GameObject NewMagic = MagicPrefab;
        Instantiate(NewMagic);
        NewMagic.GetComponent<MenuMagics>().magicNumber = 0;

        GameObject NewMagic2 = MagicPrefab;
        Instantiate(NewMagic2);
        NewMagic2.GetComponent<MenuMagics>().magicNumber = 1;

        GameObject NewMagic3 = MagicPrefab;
        Instantiate(NewMagic3);
        NewMagic3.GetComponent<MenuMagics>().magicNumber = 2;

    }

    void Update()
    {
        /*
        for (int i = 0; i < Player.GetComponent<Magics>().magics.Length && !MagicsInstantiated; i++)
        {
            GameObject NewMagic = MagicPrefab;
            Instantiate(NewMagic);
            trans.GetChild(i + 1).GetComponent<Transform>().localPosition = new Vector3(0f, 80f - (i * 40), 0f);
            //NewMagic.transform.SetParent(trans,false);
            NewMagic.GetComponent<MenuMagics>().magicNumber = i;
            //NewMagic.GetComponent<Transform>().localPosition = new Vector3(0f, 80f - (i * 40), 0f);

            if (i == PlayerMagics.magics.Length - 1)
            {
                MagicsInstantiated = true;
            }
        }
        */
    }
}
