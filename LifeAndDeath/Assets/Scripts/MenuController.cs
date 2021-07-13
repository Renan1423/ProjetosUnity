using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public GameObject LevelLoader;

    public void StartGame()
    {
        LevelLoader.GetComponent<LevelLoader>().LoadNextLevel();
    }

    public void Exit()
    {
        Application.Quit();
    }


}
