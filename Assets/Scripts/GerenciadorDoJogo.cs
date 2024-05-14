using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GerenciadorDoJogo : MonoBehaviour
{ 
    
    public ControleDosInimgiosScript controleDosInimgios;
    public ControleDeScene controleDeScene;
    
    public int InimigosLiquidados;
    public int InimigosTotais;
    public TextMeshProUGUI contadorDeInimigos;


    void Start()
    {
        controleDeScene = GameObject.Find("ControleDeScenes").GetComponent<ControleDeScene>();
        controleDosInimgios = GameObject.Find("Inimigos").GetComponent<ControleDosInimgiosScript>();
    }

    void Update()
    {
        GerenciarJogo();
    }
    
    void GerenciarJogo()
    {
        InimigosTotais = controleDosInimgios.numeroDeInimigosTotais;
        InimigosLiquidados = controleDosInimgios.numeroDeInimigosLiquidados;
        contadorDeInimigos.text = $"Número de inimigos convertidos: {InimigosLiquidados}/{InimigosTotais}";

        if (InimigosLiquidados == InimigosTotais)
        {
            controleDeScene.venceu = true;
        }
    }
    
}
