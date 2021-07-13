using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EtapaChanger : MonoBehaviour
{

    private string textoChangerEtapa;
    private string textoChangerProf;

    public Text texto;
    public Text texto2;

    public Data data;

    void Update()
    {
        if (data.etapaEnum == Etapas.FUNDII)
        {
            textoChangerEtapa = "Você mudou de etapa! Agora você está no Ensino Fundamental 2! Sua vida mudará ";
        }
        else if (data.etapaEnum == Etapas.ENSINOMEDIO)
        {
            textoChangerEtapa = "Você mudou de etapa! Agora você está no Ensino Médio! Sua vida mudará ";
        }
        else if (data.etapaEnum == Etapas.FACULDADE)
        {
            textoChangerEtapa = "Você mudou de etapa! Agora você está na Faculdade! Sua vida mudará ";
        }
        else if (data.etapaEnum == Etapas.MESTRADO)
        {
            textoChangerEtapa = "Você mudou de etapa! Agora você está no Mestrado! Sua vida mudará ";
        }
        else if (data.etapaEnum == Etapas.DOUTORADO)
        {
            textoChangerEtapa = "Você mudou de etapa! Agora você está no Doutorado! Sua vida mudará ";
        }
        else if (data.etapaEnum == Etapas.POSDOUTORADO)
        {
            textoChangerEtapa = "Você mudou de etapa! Agora você está no Pós-Doutorado! Sua vida mudará ";
        }

        texto.text = textoChangerEtapa;

        if (data.etapaEnum == Etapas.FACULDADE)
        {
            textoChangerProf = "Agora você tem uma profissão! Você é um " + data.Prof.text;
        }
        else if(data.etapaEnum != Etapas.FACULDADE)
        {
            textoChangerProf = "";
        }

        texto2.text = textoChangerProf;
    }
}
