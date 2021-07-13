using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject GameOverPanel;

    void Start()
    {
        GameOverPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Reiniciar() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
