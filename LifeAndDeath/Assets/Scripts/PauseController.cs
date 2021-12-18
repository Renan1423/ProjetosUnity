using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject EndPanel;
    public GameObject EffectsScreen;
    public Animator transition;

    public float transitionTime = 1f;

    public void Resume() {
        EffectsScreen.SetActive(true);
        Time.timeScale = 1;
        PausePanel.SetActive(false);
    }

    public void Pause()
    {
        EffectsScreen.SetActive(false);
        Time.timeScale = 0;
        PausePanel.SetActive(true);
    }

    public void ReturnMenu() {
        EffectsScreen.SetActive(true);
        Time.timeScale = 1;
        StartCoroutine(LoadMenu(0));
    }

    public void EndStage() {
        EffectsScreen.SetActive(false);
        EndPanel.SetActive(true);

    }

    public void NextStage()
    {
        EffectsScreen.SetActive(true);
        Time.timeScale = 1;
        StartCoroutine(LoadMenu(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadMenu(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
