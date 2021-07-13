using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Materias : MonoBehaviour
{

    public int levelMateria;
    public Text levelText;

    public float expTotal;
    public float expTotalBonus;
    public float expAtual;
    public float exp;
    public float somaExpMateria;

    public Data dataGame;
    public Energia energia;

    public Animator descansarAnimator;

    public Text descricao;
    public Animator descricaoAnim;

    public string descricaoBotao;

    private Animator anim;

    public GameObject SceneEffects;

    private bool EtapaChanged;

    void Start()
    {
        anim = GetComponent<Animator>();    
    }

    void Update()
    {
        if (energia.energyValue <= energia.energyCost)
        {
            descansarAnimator.SetBool("Zoom", true);
        }
        else {
            descansarAnimator.SetBool("Zoom", false);
        }
    }

    public void ClickMateria()
    {
        AudioController.current.PlayMusic(AudioController.current.click);
        if (!dataGame.Clicked)
        {
            if (energia.energyValue >= energia.energyCost)
            {
                if (dataGame.mes >= 12) {
                    StartCoroutine(dataGame.AnimChangeSerie());
                    //StartCoroutine(dataGame.AnimChangeEtapa());
                    if (dataGame.ano == 5 && EtapaChanged == false)
                    {
                        StartCoroutine(dataGame.EtapaChangerOn());
                        EtapaChanged = true;
                    }
                    else if (dataGame.ano == 9 && EtapaChanged == true)
                    {
                        StartCoroutine(dataGame.EtapaChangerOn());
                        EtapaChanged = false;
                    }
                    else if (dataGame.ano == 12 && EtapaChanged == false)
                    {
                        StartCoroutine(dataGame.EtapaChangerOn());
                        EtapaChanged = true;
                    }
                    else if (dataGame.ano == 17 && EtapaChanged == true)
                    {
                        StartCoroutine(dataGame.EtapaChangerOn());
                        EtapaChanged = false;
                    }
                    else if (dataGame.ano == 19 && EtapaChanged == false)
                    {
                        StartCoroutine(dataGame.EtapaChangerOn());
                        EtapaChanged = true;
                    }
                    else if (dataGame.ano == 21 && EtapaChanged == true)
                    {
                        StartCoroutine(dataGame.EtapaChangerOn());
                        EtapaChanged = false;
                    }
                    else
                    {

                    }
                }
                StartCoroutine(dataGame.MudancaDeSerie());
                StartCoroutine(dataGame.MudancaDeEtapa());
                dataGame.mes++;

                StartCoroutine(dataGame.AnimCharge());
                expAtual += exp;
                somaExpMateria += exp;
                energia.energyValue -= energia.energyCost;

                if (expAtual >= expTotal)
                {
                    expAtual = 0;
                    

                    StartCoroutine(dataGame.AnimLevelUp());
                    StartCoroutine(dataGame.EmotionHappy());
                    levelMateria++;
                    levelText.text = "" + levelMateria;

                    expTotal += expTotalBonus;
                }
                dataGame.Clicked = true;

                
            }
            else
            {
                StartCoroutine(dataGame.EmotionSad());
            }
        }
        else {
            // Sem energia!
        }

    }

    public void ClickRest() {

        AudioController.current.PlayMusic(AudioController.current.click);

        if (energia.energyValue == energia.maxEnergyValue)
        {

        }
        else {
            SceneEffects.GetComponent<SceneEffects>().FadeInLevel();
            
            energia.energyValue = energia.maxEnergyValue;

            if (dataGame.mes >= 12)
            {
                StartCoroutine(dataGame.AnimChangeSerie());
                if (dataGame.ano == 5 && EtapaChanged == false)
                {
                    StartCoroutine(dataGame.EtapaChangerOn());
                    EtapaChanged = true;
                }
                else if (dataGame.ano == 9 && EtapaChanged == true)
                {
                    StartCoroutine(dataGame.EtapaChangerOn());
                    EtapaChanged = false;
                }
                else if (dataGame.ano == 12 && EtapaChanged == false)
                {
                    StartCoroutine(dataGame.EtapaChangerOn());
                    EtapaChanged = true;
                }
                else if (dataGame.ano == 17 && EtapaChanged == true)
                {
                    StartCoroutine(dataGame.EtapaChangerOn());
                    EtapaChanged = false;
                }
                else if (dataGame.ano == 19 && EtapaChanged == false)
                {
                    StartCoroutine(dataGame.EtapaChangerOn());
                    EtapaChanged = true;
                }
                else if (dataGame.ano == 21 && EtapaChanged == true)
                {
                    StartCoroutine(dataGame.EtapaChangerOn());
                    EtapaChanged = false;
                }
                else
                {

                }
            }
            StartCoroutine(dataGame.MudancaDeSerie());
            dataGame.mes++;
        }
    }

    public void ButtonSelected() {
        StartCoroutine(ButtonSelecSound());
        anim.SetBool("Over", true);
        descricaoAnim.SetBool("IsSelected", true);
        descricao.text = descricaoBotao;
    }

    IEnumerator ButtonSelecSound() {
        yield return new WaitForSeconds(0.4f);

        AudioController.current.PlayMusic(AudioController.current.moveButton);
    }

    public void ButtonNotSelected()
    {
        StartCoroutine(ButtonNotSelecSound());
        anim.SetBool("Over", false);
        descricaoAnim.SetBool("IsSelected", false);
        descricao.text = "";
    }

    IEnumerator ButtonNotSelecSound()
    {
        yield return new WaitForSeconds(0.4f);

        AudioController.current.PlayMusic(AudioController.current.moveButtonBack);
    }
}
