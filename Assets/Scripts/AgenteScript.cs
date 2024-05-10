using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgenteScript : MonoBehaviour
{
    NavMeshAgent AI;
    BossScript bossScript;
    public Transform player;
    public float distanciaDeAtaque;
    public float velocidadeDeGiro = 5;
    public float velocidadeDoAgente;

    public Transform gatilho;
    public GameObject ProjetilPreFab;
    public float velocidadeDoAtaque = 10;
    public float distanciaMaximaDoAtaque = 10;
    float tempoDeVidaDoAtaque;
    public int danoCausado = 5;
    public float frequenciaDoTiro;
    [System.NonSerialized] public float timer = 0;
    public int vida;

    float distanciaPlayerAgente;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        
        bossScript = GameObject.Find("Boss").GetComponent<BossScript>();

        AI = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        distanciaPlayerAgente = Vector3.Distance(transform.position, player.position);

        if (distanciaPlayerAgente <= distanciaDeAtaque)
        {
            Atacar();
        }
        else
        {
            Perseguir();
        }
    }
    void Perseguir()
    {
        AI.isStopped = false;
        AI.stoppingDistance = distanciaDeAtaque;
        AI.speed = velocidadeDoAgente;
        AI.SetDestination(player.position);
    }
    void Atacar()
    {
        AI.isStopped = true;
        Rotacionar();
        Atirar();
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
            GameObject ataque = Instantiate(ProjetilPreFab, gatilho.position, gatilho.rotation);
            ataque.GetComponent<Rigidbody>().velocity = gatilho.forward * velocidadeDoAtaque;

            timer = 0;

            tempoDeVidaDoAtaque = distanciaMaximaDoAtaque / velocidadeDoAtaque;
            Destroy(ataque, tempoDeVidaDoAtaque);
        }
        timer += Time.deltaTime;
    }
    public void AplicarDanoNoAgente(int dano)
    {
        dano = vida;
        vida -= dano;        

        if (vida <= 0)
        {
            gameObject.SetActive(false);
            bossScript.numeroDeAgentes -= 1;
        }
    }
}

