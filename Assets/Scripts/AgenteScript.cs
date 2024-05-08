using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgenteScript : MonoBehaviour
{
    NavMeshAgent AI;
    Boss_PlayerScript boss_playerScript;
    BossScript bossScript;
    public Transform boss_player;
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
        boss_player = GameObject.FindWithTag("Player").transform;
        AI = GetComponent<NavMeshAgent>();

        boss_playerScript = GameObject.Find("Player").GetComponent<Boss_PlayerScript>();
        bossScript = GameObject.Find("Inimigo").GetComponent<BossScript>();
    }

    void Update()
    {
        distanciaPlayerAgente = Vector3.Distance(transform.position, boss_player.position);

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
        AI.SetDestination(boss_player.position);
    }
    void Atacar()
    {
        AI.isStopped = true;
        Rotacionar();
        Atirar();
    }
    void Rotacionar()
    {
        Vector3 direction = (boss_player.position - transform.position).normalized;
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
    public void AplicarDanoNoInimigo(int dano)
    {
        dano = vida;
        vida -= dano;        

        if (vida <= 0)
        {
            Destroy(this.gameObject);
            bossScript.numeroDeAgentesLiquidados += 1;
        }
    }
}

