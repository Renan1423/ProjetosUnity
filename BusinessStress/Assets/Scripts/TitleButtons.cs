using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButtons : MonoBehaviour
{
    public GameObject TitlePanel;

    public GameObject CreditsPanel;
    public GameObject TutorialPanel;

    public GameObject Cutscene;

    private GameObject player;
    public GameObject playerUi;

    private int bgmnull = 0;

    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Player>().isCutscene = true;
        

    }

    void Update()
    {

    }

    public void PlayGame() {
        AudioController.current.PlayMusic(AudioController.current.punchHeavy);
        StartCoroutine(STARTGAME());
    }

    IEnumerator STARTGAME() {

        
        Cutscene.GetComponent<Animator>().SetTrigger("Cutscene");
        

        yield return new WaitForSeconds(0.6f);

        player.GetComponent<Player>().isCutscene = false;
        playerUi.SetActive(true);

        Destroy(Cutscene);
        gameObject.SetActive(false);

    }

    public void ExitGame() {
        AudioController.current.PlayMusic(AudioController.current.punchHeavy);
        Application.Quit();
    }

    public void OpenCredits() {
        AudioController.current.PlayMusic(AudioController.current.punchHeavy);
        CreditsPanel.SetActive(true);
    }

    public void OpenTutorial() {
        AudioController.current.PlayMusic(AudioController.current.punchHeavy);
        TutorialPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        AudioController.current.PlayMusic(AudioController.current.punchHeavy);
        CreditsPanel.SetActive(false);
    }

    public void CloseTutorial()
    {
        AudioController.current.PlayMusic(AudioController.current.punchHeavy);
        TutorialPanel.SetActive(false);
    }
}
