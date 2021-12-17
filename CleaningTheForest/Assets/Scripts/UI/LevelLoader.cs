using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public GameObject CreditsPanel;
    private bool CreditsActive = false;

    public void LoadNextLevel()
    {
        AudioController.current.PlayMusic(AudioController.current.Click);
        LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadThisLevel()
    {
        AudioController.current.PlayMusic(AudioController.current.Click);
        LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadFirstLevel() {
        AudioController.current.PlayMusic(AudioController.current.Click);
        LoadLevel(SceneManager.GetActiveScene().buildIndex - 2);
    }

    public void ExitGame() {
        AudioController.current.PlayMusic(AudioController.current.Click);
        Application.Quit();
    }

    public void OpenCloseCredits() {
        AudioController.current.PlayMusic(AudioController.current.Click);
        if (!CreditsActive)
        {
            CreditsPanel.SetActive(true);
            CreditsActive = true;
        }
        else {
            CreditsPanel.SetActive(false);
            CreditsActive = false;
        }
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
}
