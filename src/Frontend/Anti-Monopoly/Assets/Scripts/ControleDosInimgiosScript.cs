using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControleDosInimgiosScript : MonoBehaviour
{
    
    public int _frequenciaDoTiro, _danoCausado, _danoSofrido, _vidaMaxima;
    public float _velocidadeAoAndar, _velocidadeAoCorrer;

    public GameObject[] inimigos;
    InimgoScript[] inimigosScripts;
    public int numeroDeInimigosTotais;
    public int numeroDeInimigosLiquidados;
    public TextMeshProUGUI contadorDeInimigosText;
    

    PlayerScript playerScript;

    [System.NonSerialized] public ControleDeScene controleDeScene;

    private void Awake()
    {
        controleDeScene = GameObject.Find("ControleDeScenes").GetComponent<ControleDeScene>();
        contadorDeInimigosText = GameObject.Find("ContadorDeInimigos").GetComponent<TextMeshProUGUI>();
        playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
        CaptarInimigos();
    }
    void Update()
    {
        GerenciarInimigos();
        ContadorDeInimgios();
        playerScript.AlcanceMaximo(false);
    }
    void CaptarInimigos()
    {
        inimigos = GameObject.FindGameObjectsWithTag("Inimigo");
        inimigosScripts = new InimgoScript[inimigos.Length];
        for (int i = 0; i < inimigosScripts.Length; i++)
        {
            inimigosScripts[i] = inimigos[i].GetComponent<InimgoScript>();
        }
        numeroDeInimigosTotais = inimigos.Length;
        numeroDeInimigosLiquidados = 0;
    }
    void GerenciarInimigos()
    {
        for (int i = 0; i < inimigosScripts.Length; i++)
        {
            inimigosScripts[i].frequenciaDoTiro = _frequenciaDoTiro;
            inimigosScripts[i].danoCausado = _danoCausado;
                playerScript.danoSofrido = _danoCausado;
            inimigosScripts[i].vidaMaxima = _vidaMaxima;
                _danoSofrido = playerScript.danoCausado;
            inimigosScripts[i].danoSofrido = playerScript.danoCausado;
            inimigosScripts[i].velocidadeAoAndar = _velocidadeAoAndar;
            inimigosScripts[i].velocidadeAoCorrer = _velocidadeAoCorrer;
        }

        if(numeroDeInimigosTotais == numeroDeInimigosLiquidados)
        {
            controleDeScene.venceu = true;
        }        
    }
    void ContadorDeInimgios()
    {
        contadorDeInimigosText.text = $"Inimigos Liquidados: {numeroDeInimigosLiquidados}/{numeroDeInimigosTotais}";
    }
}
