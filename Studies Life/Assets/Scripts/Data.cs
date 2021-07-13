using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Etapas {FUNDI, FUNDII, ENSINOMEDIO, FACULDADE, MESTRADO, DOUTORADO, POSDOUTORADO}

public class Data : MonoBehaviour
{

    public int mes;
    public int ano;

    public Text dataAtual;

    public Etapas etapaEnum;

    public Text Serie;
    public Text Etapa;
    public Text Prof;

    public Materias MAT;
    public Materias PORT;
    public Materias EXA;
    public Materias HUM;
    public Materias ART;

    public Transform BarPoint;
    public Transform PlayerPoint;
    public Transform ProfTextTrans;
    public Transform SerieTextTrans;
    public Transform EtapaTextTrans;
    public GameObject LevelUpPrefab;
    public GameObject BarPrefab;
    public GameObject FirePrefab;
    public GameObject StarPrefab;

    public Animator PlayerAnimator;
    public Animator SerieTextAnimator;
    public Animator EtapaTextAnimator;
    public Animator DescricoesAnimator;

    public bool Clicked = false;

    private float ExpCompleto;
    private bool Matematica;
    private bool Portugues;
    private bool Exatas;
    private bool Humanas;
    private bool Artes;

    public GameObject EtapaChanger;


    void Start()
    {
        DataCheck();
    }


    void Update()
    {
        MudancaDeAno();
        DataCheck();
        StartCoroutine(MudancaDeEtapa());
        JobCheck();

    }

    public IEnumerator AnimLevelUp() {

        yield return new WaitForSeconds(1.5f);

        AudioController.current.PlayMusic(AudioController.current.levelUp1);
        AudioController.current.PlayMusic(AudioController.current.levelUp2);
        Instantiate(LevelUpPrefab, BarPoint);
    }

    public IEnumerator AnimChangeSerie() {

        DescricoesAnimator.SetBool("Over", true);

        yield return new WaitForSeconds(1f);

        SerieTextAnimator.SetBool("BarOn", true);

        yield return new WaitForSeconds(0.9f);

        SerieTextAnimator.SetBool("BarOn", false);

        yield return new WaitForSeconds(0.1f);

        Instantiate(StarPrefab, SerieTextTrans);
        AudioController.current.PlayMusic(AudioController.current.starSound);

        yield return new WaitForSeconds(1.5f);

        DescricoesAnimator.SetBool("Over", false);
    }

    public IEnumerator AnimChangeEtapa() {

            DescricoesAnimator.SetBool("Over", true);

            yield return new WaitForSeconds(1f);

            EtapaTextAnimator.SetBool("BarOn", true);

            yield return new WaitForSeconds(0.9f);

            EtapaTextAnimator.SetBool("BarOn", false);

            yield return new WaitForSeconds(0.1f);

            Instantiate(StarPrefab, EtapaTextTrans);

            yield return new WaitForSeconds(1.5f);

            DescricoesAnimator.SetBool("Over", false);
    }

    public IEnumerator AnimChangeProf()
    {

        yield return new WaitForSeconds(0.1f);

        Instantiate(StarPrefab, ProfTextTrans);
    }

    public IEnumerator AnimCharge() {
        Instantiate(BarPrefab, BarPoint);
        PlayerAnimator.SetBool("Super", true);
        Instantiate(FirePrefab, PlayerPoint);

        yield return new WaitForSeconds(1.5f);

        PlayerAnimator.SetBool("Super", false);
        if (Clicked == true)
        {
            StartCoroutine(ClickedFalse());
        }


    }

    public IEnumerator ClickedFalse()
    {
        yield return new WaitForSeconds(1f);
        Clicked = false;
    }

    public IEnumerator EmotionHappy() {

        yield return new WaitForSeconds(1.5f);
        
        PlayerAnimator.SetBool("Super", false);
        PlayerAnimator.SetBool("Happy", true);

        yield return new WaitForSeconds(1f);

        PlayerAnimator.SetBool("Happy", false);
    }

    public IEnumerator EmotionSad()
    {
        PlayerAnimator.SetBool("Sad", true);

        yield return new WaitForSeconds(1f);

        PlayerAnimator.SetBool("Sad", false);
    }

    void JobCheck() {
        if (etapaEnum == Etapas.FACULDADE || etapaEnum == Etapas.MESTRADO || etapaEnum == Etapas.DOUTORADO || etapaEnum == Etapas.POSDOUTORADO)
        {

            ExpCompleto = MAT.somaExpMateria + PORT.somaExpMateria + EXA.somaExpMateria + HUM.somaExpMateria + ART.somaExpMateria;

            if (MAT.somaExpMateria >= ExpCompleto / 3.6f)
            {
                Matematica = true;
                Debug.Log("Matemática!");
            }
            if (PORT.somaExpMateria >= ExpCompleto / 3.6f)
            {
                Portugues = true;
                Debug.Log("Português!");
            }
            if (EXA.somaExpMateria >= ExpCompleto / 3.6f)
            {
                Exatas = true;
                Debug.Log("Exatas!");
            }
            if (HUM.somaExpMateria >= ExpCompleto / 3.6f)
            {
                Humanas = true;
                Debug.Log("Humanas!");
            }
            if (ART.somaExpMateria >= ExpCompleto / 3.6f)
            {
                Artes = true;
                Debug.Log("Artes!");
            }

            if (Matematica)
            {
                Prof.text = "MATEMÁTICO";
                if (Matematica && Exatas)
                {
                    Prof.text = "ENGENHEIRO";
                }
                if (Matematica && Exatas && Artes)
                {
                    Prof.text = "GAME DEV";
                }
                else if (Humanas && Matematica)
                {
                    Prof.text = "GEÓGRAFO";
                }
                else if (Artes && Portugues && Matematica)
                {
                    Prof.text = "CINEASTA";
                }
                else if (Matematica && Portugues && Exatas && Humanas && Artes)
                {
                    Prof.text = "PROFESSOR";
                }
            }

            else if (Portugues)
            {
                Prof.text = "ESCRITOR";
                if (Portugues && Humanas)
                {
                    Prof.text = "POLÍTICO";
                }
                else if (Portugues && Humanas && Artes)
                {
                    Prof.text = "JORNALISTA";
                }
                else if (Artes && Portugues && Matematica)
                {
                    Prof.text = "CINEASTA";
                }
                else if (Matematica && Portugues && Exatas && Humanas && Artes)
                {
                    Prof.text = "PROFESSOR";
                }
            }

            else if (Humanas)
            {
                Prof.text = "HISTORIADOR";
                if (Humanas && Matematica)
                {
                    Prof.text = "GEÓGRAFO";
                }
                else if (Portugues && Humanas)
                {
                    Prof.text = "POLÍTICO";
                }
                else if (Portugues && Humanas && Artes)
                {
                    Prof.text = "JORNALISTA";
                }
                else if (Humanas && Artes)
                {
                    Prof.text = "ESTILISTA";
                }
                else if (Matematica && Portugues && Exatas && Humanas && Artes)
                {
                    Prof.text = "PROFESSOR";
                }
            }

            else if (Exatas)
            {
                Prof.text = "FÍSICO";
                if (Exatas && Humanas)
                {
                    Prof.text = "MÉDICO";
                }
                else if (Matematica && Exatas)
                {
                    Prof.text = "ENGENHEIRO";
                }
                else if (Matematica && Exatas && Artes)
                {
                    Prof.text = "GAME DEV";
                }
                else if (Artes && Exatas)
                {
                    Prof.text = "MÚSICO";
                }
                else if (Exatas && Artes)
                {
                    Prof.text = "ARQUITETO";
                }
                else if (Matematica && Portugues && Exatas && Humanas && Artes)
                {
                    Prof.text = "PROFESSOR";
                }
            }

            else if (Artes)
            {
                Prof.text = "ILUSTRADOR";
                if (Artes && Portugues && Matematica)
                {
                    Prof.text = "CINEASTA";
                }
                else if (Matematica && Exatas && Artes)
                {
                    Prof.text = "GAME DEV";
                }
                else if (Artes && Exatas)
                {
                    Prof.text = "MÚSICO";
                }
                else if (Artes && Humanas)
                {
                    Prof.text = "QUADRINISTA";
                }
                else if (Matematica && Portugues && Exatas && Humanas && Artes)
                {
                    Prof.text = "PROFESSOR";
                }

        }

    }


    }

    public IEnumerator MudancaDeSerie() {

        yield return new WaitForSeconds(2f);

        if (ano == 1) 
        {
            Serie.text = "PRIMEIRO ANO";
        }
        else if (ano == 2)
        {
            Serie.text = "SEGUNDO ANO";
        }
        else if (ano == 3)
        {
            Serie.text = "TERCEIRO ANO";
        }
        else if (ano == 4)
        {
            Serie.text = "QUARTO ANO";
        }
        else if (ano == 5)
        {
            Serie.text = "QUINTO ANO";
        }
        else if (ano == 6)
        {
            Serie.text = "SEXTO ANO";
        }
        else if (ano == 7)
        {
            Serie.text = "SÉTIMO ANO";
        }
        else if (ano == 8)
        {
            Serie.text = "OITAVO ANO";
        }
        else if (ano == 9)
        {
            Serie.text = "NONO ANO";
        }
        else if (ano == 10)
        {
            Serie.text = "PRIMEIRO ANO";
        }
        else if (ano == 11)
        {
            Serie.text = "SEGUNDO ANO";
        }
        else if (ano == 12)
        {
            Serie.text = "TERCEIRO ANO";
        }
        else if (ano == 13)
        {
            Serie.text = "PRIMEIRO ANO";
        }
        else if (ano == 14)
        {
            Serie.text = "SEGUNDO ANO";
        }
        else if (ano == 15)
        {
            Serie.text = "TERCEIRO ANO";
        }
        else if (ano == 16)
        {
            Serie.text = "QUARTO ANO";
        }
        else if (ano == 17)
        {
            Serie.text = "QUINTO ANO";
        }
        else if (ano == 18)
        {
            Serie.text = "PRIMEIRO ANO";
        }
        else if (ano == 19)
        {
            Serie.text = "SEGUNDO ANO";
        }
        else if (ano == 20)
        {
            Serie.text = "PRIMEIRO ANO";
        }
        else if (ano == 20)
        {
            Serie.text = "SEGUNDO ANO";
        }
        else if (ano == 21)
        {
            Serie.text = "PRIMEIRO ANO";
        }
        else if (ano == 22)
        {
            Serie.text = "SEGUNDO ANO";
        }
    }

    void DataCheck() {
        if (mes < 10 && ano < 10)
        {
            dataAtual.text = "0" + mes + "/0" + ano;
        }
        else if (mes < 10 && ano >= 10)
        {
            dataAtual.text = "0" + mes + "/" + ano;
        }
        else if (mes >= 10 && ano < 10)
        {
            dataAtual.text = mes + "/0" + ano;
        }
        else
        {
            dataAtual.text = mes + "/" + ano;
        }
    }

    void MudancaDeAno() {
        if(mes > 12)
        {
            ano++;
            mes = 1;
        }
    }

    public IEnumerator MudancaDeEtapa() {

        yield return new WaitForSeconds(2f);

        if (ano == 1)
        {
            etapaEnum = Etapas.FUNDI;
        }
        else if (ano == 6)
        {
            etapaEnum = Etapas.FUNDII;
        }
        else if (ano == 10)
        {
            etapaEnum = Etapas.ENSINOMEDIO;
        }
        else if (ano == 13)
        {
            etapaEnum = Etapas.FACULDADE;
        }
        else if (ano == 18)
        {
            etapaEnum = Etapas.MESTRADO;
        }
        else if (ano == 20)
        {
            etapaEnum = Etapas.DOUTORADO;
        }
        else if (ano == 22)
        {
            etapaEnum = Etapas.POSDOUTORADO;
        }

        //if (ano <= 5)
        //{
        //    etapaEnum = Etapas.FUNDI;
        //}
        //else if (ano > 5 && ano <= 9)
        //{
        //    etapaEnum = Etapas.FUNDII;
        //}
        //else if (ano > 9 && ano <= 12)
        //{
        //    etapaEnum = Etapas.ENSINOMEDIO;
        //}
        //else if (ano > 12 && ano <= 17)
        //{
        //    etapaEnum = Etapas.FACULDADE;
        //}
        //else if (ano > 17 && ano <= 19)
        //{
        //    etapaEnum = Etapas.MESTRADO;
        //}
        //else if (ano > 19 && ano <= 21)
        //{
        //    etapaEnum = Etapas.DOUTORADO;
        //}
        //else if (ano > 21) {
        //    etapaEnum = Etapas.POSDOUTORADO;
        //}


        if (etapaEnum == Etapas.FUNDI)
        {
            Etapa.text = "FUNDAMENTAL I";
        }
        else if (etapaEnum == Etapas.FUNDII)
        {
            Etapa.text = "FUNDAMENTAL II";
        }
        else if (etapaEnum == Etapas.ENSINOMEDIO)
        {
            Etapa.text = "ENSINO MÉDIO";
        }
        else if (etapaEnum == Etapas.FACULDADE)
        {
            Etapa.text = "FACULDADE";
        }
        else if (etapaEnum == Etapas.MESTRADO)
        {
            Etapa.text = "MESTRADO";
        }
        else if (etapaEnum == Etapas.DOUTORADO)
        {
            Etapa.text = "DOUTORADO";
        }
        else if (etapaEnum == Etapas.POSDOUTORADO) {
            Etapa.text = "POSDOUTORADO";
        }
    }

    public IEnumerator EtapaChangerOn() {

        yield return new WaitForSeconds(3f);

        EtapaChanger.SetActive(true);
    }
}
