using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public float Velocidade;

    public Transform player;

    public int vidaMaxima = 4;
    public int vidaAtual;
    public BarraDeVidaScript barraDeVida;
    public int danoSofrido = 1;

    public GameObject[] spawnDosAgentes;
    public GameObject agentePreFab;
    public int numeroDeAgentes;
    public bool convocarAgentes = false;
    float timer;
    int frequencia;
    private void Awake()
    {
        spawnDosAgentes = GameObject.FindGameObjectsWithTag("SpawnDeAgentes");
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Start()
    {
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
        transform.rotation = Quaternion.RotateTowards(transform.rotation, anguloDoPlayer, Velocidade * Time.deltaTime);
    }
    void GerenciarReacaoDoBoss()
    {
        if(convocarAgentes)
        {
            ConvocarAgentes();
            convocarAgentes = false;
        }
        
    }
    void ConvocarAgentes()
    {
        for (int i = 0; i < spawnDosAgentes.Length; i++)
        {
            GameObject agente = Instantiate(agentePreFab, spawnDosAgentes[i].transform.position, spawnDosAgentes[i].transform.rotation);
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
    private void OnCollisionEnter(Collision collision)
    {
        convocarAgentes = true;
    }
}
