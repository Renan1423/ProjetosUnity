using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    public LevelLoader levelLoader;
    private Transform trans;

    public GameObject CreditsScreen;

    public GameObject TutorialScreen;
    public GameObject TutorialScreen2;

    private void Start()
    {
        trans = GetComponent<Transform>();
    }

    public void ReturnMainMenu() {
        levelLoader.LoadFirstLevel();
        AudioController.current.PlayMusic(AudioController.current.buttonSoundSFX);
    }

    public void StartGame()
    {
        levelLoader.LoadNextLevel();
        AudioController.current.PlayMusic(AudioController.current.buttonSoundSFX);
    }

    public void ShowCredits() {
        AudioController.current.PlayMusic(AudioController.current.buttonSoundSFX);
        CreditsScreen.SetActive(true);
    }

    public void CloseCredits()
    {
        AudioController.current.PlayMusic(AudioController.current.buttonSoundSFX);
        CreditsScreen.SetActive(false);
    }

    public void ShowTutorial() {
        AudioController.current.PlayMusic(AudioController.current.buttonSoundSFX);
        TutorialScreen.SetActive(true);
    }

    public void ShowTutorial2() {
        AudioController.current.PlayMusic(AudioController.current.buttonSoundSFX);
        TutorialScreen2.SetActive(true);
    }

    public void CloseTutorial() {
        AudioController.current.PlayMusic(AudioController.current.buttonSoundSFX);
        TutorialScreen.SetActive(false);
        TutorialScreen2.SetActive(false);
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void OnOver()
    {
        trans.localScale = new Vector2(2.5f, 2.5f);
    }

    public void NotOnOver()
    {
        trans.localScale = new Vector2(2f, 2f);
    }

}
