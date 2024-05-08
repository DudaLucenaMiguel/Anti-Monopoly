using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public int vidaMaxima = 50;
    public int vidaAtual;
    public BarraDeVidaScript barraDeVida;
    public int danoSofrido;
    PlayerScript playerScript;

    [System.NonSerialized] public int numeroDeAgentesLiquidados;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void AplicarDanoNoBoss(int dano)
    {
        dano = danoSofrido;
        vidaAtual -= dano;
        barraDeVida.AlterarBarraDeVida(vidaAtual, vidaMaxima);

        if (vidaAtual <= 0)
        {
            gameObject.SetActive(false);
            playerScript.somaDeInimigosConvertidos += 1;
        }
    }
}
