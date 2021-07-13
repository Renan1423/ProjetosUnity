using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEffects : MonoBehaviour
{

    public Animator transition;

    public int typePanel; // 1 para o que tem fadeout, 0 para normal

    public float transitionTime = 1f;

    void Start()
    {
        if (typePanel == 1)
        {
            transition.SetInteger("Effects", 2);
            FadeOutLevel();
        }
        else if (typePanel == 0)
        {
            gameObject.SetActive(false);
        }
        else if (typePanel == 2) {
            gameObject.SetActive(true);
        }
    }

    public void FadeOutLevel()
    {
        StartCoroutine(FadeOutLevelCoroutine());
    }

    IEnumerator FadeOutLevelCoroutine()
    {
        transition.SetInteger("Effects", 2);

        yield return new WaitForSeconds(1f);

        transition.SetInteger("Effects", 0);
        gameObject.SetActive(false);

    }

    public void FadeInLevel()
    {
        StartCoroutine(FadeInLevelCoroutine());
    }

    IEnumerator FadeInLevelCoroutine()
    {
        transition.SetInteger("Effects", 1);

        yield return new WaitForSeconds(transitionTime);

        transition.SetInteger("Effects", 0);
    }
}
