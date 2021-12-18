using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCount : MonoBehaviour
{

    static public int floor = 50;
    public int numberOfEnemies;

    public Text EnemiesCountText;
    public Text EnemiesCountShadowText;

    public Text FloorUI;
    public Text FloorUIShadow;
    public Text FloorLoading;

    public LevelLoader LevelLoader;
    public GameObject AstarObject;

    public bool UpgradeSelected = false;

    void Start()
    {
        StartCoroutine(ActiveAstar());   
    }

    void Update()
    {
        EnemiesCountText.text = "" + numberOfEnemies;
        EnemiesCountShadowText.text = "" + numberOfEnemies;

        StartCoroutine(CheckNumber());
        

        FloorLoading.text = "Floor " + floor;
        FloorUI.text = "" + floor;
        FloorUIShadow.text = "" + floor;
    }

    IEnumerator CheckNumber() {
        yield return new WaitForSeconds(4f);

        if (numberOfEnemies <= 0)
        {
            //LevelLoader.LoadThisLevel();
            StartCoroutine(onEndFloor());
        }
    }

    IEnumerator onEndFloor() {
        AudioController.current.PlayMusic(AudioController.current.evilLaughSFX);

        yield return new WaitForSeconds(2f);

        LevelLoader.FadeEnd();
    }

    IEnumerator ActiveAstar() {

        yield return new WaitForSeconds(3f);

        AstarObject.SetActive(true);
    }
}
