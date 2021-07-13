using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEffects : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    public void FadeReviveLevel()
    {
        StartCoroutine(FadeInLevelCoroutine());
    }

    public void FadeDieLevel() 
    {
        StartCoroutine(FadeOutLevelCoroutine());
    }

    IEnumerator FadeInLevelCoroutine()
    {
        transition.SetInteger("Effects", 1);

        yield return new WaitForSeconds(transitionTime);

        transition.SetInteger("Effects", 0);
    }

    IEnumerator FadeOutLevelCoroutine()
    {
        transition.SetInteger("Effects", 2);

        yield return new WaitForSeconds(transitionTime);

        transition.SetInteger("Effects", 0);
    }
}
