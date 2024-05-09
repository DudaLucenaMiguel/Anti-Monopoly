using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public float velocidadeDoBoss;
    public Transform player;

    public int vidaMaxima = 4;
    public int vidaAtual;
    public BarraDeVidaScript barraDeVida;
    public int danoSofrido;
    PlayerScript playerScript;

    public GameObject agentePreFab;
    public GameObject[] spawnDosAgentes;
    public int numeroDeAgentes;
    public bool convocarAgentes;
    float timer;
    int frequencia;


    void Start()
    {
        spawnDosAgentes = GameObject.FindGameObjectsWithTag("AgenteSpawn");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        vidaAtual = vidaMaxima;
        barraDeVida.AlterarBarraDeVida(vidaAtual, vidaMaxima);
    }

    void Update()
    {
        OlharParaOPlayer();
        GerenciarReacaoDoBoss();
    }
    
    void OlharParaOPlayer()
    {
        Quaternion anguloDoPlayer = Quaternion.LookRotation(player.position - transform.position).normalized;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, anguloDoPlayer, velocidadeDoBoss * Time.deltaTime);
    }
    void GerenciarReacaoDoBoss()
    {
        if(vidaAtual == vidaMaxima - 1)
        {
            convocarAgentes = true;
            numeroDeAgentes = spawnDosAgentes.Length;
            SpawnDeAgentes();
        }
        convocarAgentes = false;
    }
    void SpawnDeAgentes()
    {
        for (int i = 0; i < spawnDosAgentes.Length; i++)
        {
            GameObject agenteClone = Instantiate(agentePreFab, spawnDosAgentes[i].transform.position, spawnDosAgentes[i].transform.rotation);
            convocarAgentes = false;
        }
        
            
    }
    public void AplicarDanoNoBoss(int dano)
    {
        dano = danoSofrido;
        vidaAtual -= dano;
        barraDeVida.AlterarBarraDeVida(vidaAtual, vidaMaxima);

        if (vidaAtual <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
