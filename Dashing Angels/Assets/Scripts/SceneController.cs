using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject GameOverPanel;

    public int stage;
    public Text stageText;

    public static int enemiesDefeated;
    public GameObject player;

    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject spawn3;
    public GameObject spawn4;

    private float minTimeSpawn;
    private float maxTimeSpawn;

    public void Start() {
        minTimeSpawn = spawn1.GetComponent<SpawnEnemy>().minTime;
        maxTimeSpawn = spawn1.GetComponent<SpawnEnemy>().maxTime;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
    }

    public void Resume() {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
    }

    public void Exit()
    {
        //Application.Quit();
        SceneManager.LoadScene(0);
    }

    public void StartGame()
    {
       SceneManager.LoadScene(1);
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void GameOver(){
        GameOverPanel.SetActive(true);
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Update()
    {
        stageChange();
    }

    public void stageChange() {
        if (enemiesDefeated >= (10 + (2*stage)))
        {
            enemiesDefeated = 0;
            stage++;
            stageText.text = "STAGE " + stage;
            if (player.GetComponent<Player>().life < 5)
            {
                player.GetComponent<Player>().life += 1;
            }
            if (minTimeSpawn > 3)
            {
                minTimeSpawn -= 1;
            }if (maxTimeSpawn > 5) {
                maxTimeSpawn -= 1;
            }
            spawn1.GetComponent<SpawnEnemy>().minTime = minTimeSpawn;
            spawn1.GetComponent<SpawnEnemy>().maxTime = maxTimeSpawn;
            spawn2.GetComponent<SpawnEnemy>().minTime = minTimeSpawn;
            spawn2.GetComponent<SpawnEnemy>().maxTime = maxTimeSpawn;
            spawn3.GetComponent<SpawnEnemy>().minTime = minTimeSpawn;
            spawn3.GetComponent<SpawnEnemy>().maxTime = maxTimeSpawn;
            spawn4.GetComponent<SpawnEnemy>().minTime = minTimeSpawn;
            spawn4.GetComponent<SpawnEnemy>().maxTime = maxTimeSpawn;
        }
    }

}
