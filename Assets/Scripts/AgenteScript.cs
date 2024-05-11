using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class AgenteScript : MonoBehaviour
{
    NavMeshAgent AI;
    BossScript bossScript;
    public Transform player;
    float distanciaPlayerAgente;
    public float distanciaLimiteAgentePlayer = 5;
    
    public float velocidadeDeGiro = 50;
    public float velocidadeDoAgente;

    public Transform gatilho;
    public GameObject ataquePreFab;
    public float velocidadeDoAtaque = 20;
    public float alcanceDoAtaque = 10;
    float sobrevidaDoAtaque;
    public float distanciaMinimaParaAtacar = 10;
    public int danoCausado = 5;
    public float frequenciaDoTiro = 1;
    [System.NonSerialized] public float timer = 0;
    public int vida;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;

        bossScript = GameObject.Find("Boss").GetComponent<BossScript>();

        AI = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        distanciaPlayerAgente = Vector3.Distance(transform.position, player.position);

        Perseguir();

        if (distanciaPlayerAgente <= distanciaMinimaParaAtacar)
        {
            Atirar();
        }
        if(AI.isStopped)
        {
            Rotacionar();
        }
    }
    void Perseguir()
    {
        AI.stoppingDistance = distanciaLimiteAgentePlayer;
        AI.speed = velocidadeDoAgente;
        AI.SetDestination(player.position);
    }
    void Rotacionar()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, velocidadeDeGiro * Time.deltaTime);
    }
    void Atirar()
    {
        if (timer > frequenciaDoTiro)
        {
            GameObject ataque = Instantiate(ataquePreFab, gatilho.position, gatilho.rotation);
            ataque.GetComponent<Rigidbody>().velocity = gatilho.forward * velocidadeDoAtaque;

            timer = 0;

            sobrevidaDoAtaque = alcanceDoAtaque / velocidadeDoAtaque;
            Destroy(ataque, sobrevidaDoAtaque);
        }
        timer += Time.deltaTime;
    }
    public void AplicarDanoNoAgente(int dano)
    {
        dano = vida;
        vida -= dano;        

        if (vida <= 0)
        {
            bossScript.numeroDeAgentes -= 1;
            Destroy(gameObject);
        }
    }
}

