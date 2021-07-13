using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public GameObject SceneEffects;
    public SceneEffects effectsPanel;

    public void NextStage()
    {
        Time.timeScale = 1;
        StartCoroutine(LoadMenu(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void ReturnMenu()
    {
        Time.timeScale = 1;
        StartCoroutine(LoadMenu(0));
    }

    IEnumerator LoadMenu(int levelIndex)
    {
        SceneEffects.SetActive(true);
        effectsPanel.transition.SetTrigger("Start");

        yield return new WaitForSeconds(effectsPanel.transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
