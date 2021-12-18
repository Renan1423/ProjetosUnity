using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    void Update()
    {
       
    }

    public void LoadNextLevel() {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadThisLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    public void LoadFirstLevel() {
        StartCoroutine(LoadLevelNoTransition(0));
    }

    public void FadeEnd() {
        StartCoroutine(FadeEndCoroutine());   
    }

    IEnumerator LoadLevel(int levelIndex) {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

    IEnumerator LoadLevelNoTransition(int levelIndex)
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(levelIndex);
    }

    IEnumerator FadeEndCoroutine() {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
    }
}
