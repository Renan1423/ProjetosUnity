using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject pause;
    private bool ActiveMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            ActiveMenu = !ActiveMenu;
            pause.SetActive(ActiveMenu);
        }
    }

    public void startGame() {
        SceneManager.LoadScene(1);
    }

    public void exitOptionMenu(GameObject go) {
        go.SetActive(false);
    }

    public void openOptionMenu(GameObject go)
    {
        go.SetActive(true);
    }

}
