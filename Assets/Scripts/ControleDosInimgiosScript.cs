using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControleDosInimgiosScript : MonoBehaviour
{
    [Range(0, 20)] public float _raiosDeGuarda;
    [Range(0, 10)] public float _distanciaDeAtaque;
    public int _frequenciaDoTiro, _danoCausado, _danoSofrido, _vidaMaxima;

    public GameObject[] inimigos;
    InimgoScript[] inimigosScripts;
    public int numeroDeInimigosTotais;
    public int numeroDeInimigosLiquidados;
    public TextMeshProUGUI contadorDeInimigos;

    PlayerScript playerScript;

    [System.NonSerialized] public ControleDeScene controleDeScene;

    private void Awake()
    {
        controleDeScene = GameObject.Find("ControleDeScenes").GetComponent<ControleDeScene>();

        CaptarInimigos();
        CaptarPlayer();

    }
    void Update()
    {
        GerenciarInimigos();
        ContadorDeInimgios();
    }
    void CaptarPlayer()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
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
            inimigosScripts[i].raioDeGuarda = _raiosDeGuarda;
            inimigosScripts[i].distanciaDeAtaque = _distanciaDeAtaque;
            inimigosScripts[i].frequenciaDoTiro = _frequenciaDoTiro;
            inimigosScripts[i].danoCausado = _danoCausado;
                playerScript.danoSofrido = _danoCausado;
            inimigosScripts[i].vidaMaxima = _vidaMaxima;
                _danoSofrido = playerScript.danoCausado;
            inimigosScripts[i].danoSofrido = _danoSofrido;
        }

        if(numeroDeInimigosTotais == numeroDeInimigosLiquidados)
        {
            controleDeScene.venceu = true;
        }        
    }
    void ContadorDeInimgios()
    {
        contadorDeInimigos.text = $"numero de inimigos: {numeroDeInimigosLiquidados}/{numeroDeInimigosTotais}";
    }
}
