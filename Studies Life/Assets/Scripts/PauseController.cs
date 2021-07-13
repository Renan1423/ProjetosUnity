using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public GameObject PausePanel;

    public void Resume() {
        AudioController.current.PlayMusic(AudioController.current.click);
        Time.timeScale = 1;
        PausePanel.SetActive(false);
    }

    public void Pause()
    {
        AudioController.current.PlayMusic(AudioController.current.click);
        Time.timeScale = 0;
        PausePanel.SetActive(true);
    }

    public void Exit()
    {
        AudioController.current.PlayMusic(AudioController.current.click);
        Application.Quit();
    }
}
