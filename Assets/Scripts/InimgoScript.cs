using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class InimgoScript : MonoBehaviour
{
    [System.NonSerialized] NavMeshAgent AI;

    [System.NonSerialized] public Transform player;
    public Transform eixo;
    public Transform[] wayPoints;
    int index;
    float distanciaPlayerBase;
    float distanciaPlayerEnemy;

    [Range(0, 20)] public float raioDeGuarda;
    [Range(0, 10)] public float distanciaDeAtaque;
    public float velocidadeDeGiro = 5;

    public float velocidadeAoAndar = 6;
    public float velocidadeAoCorrer = 9;

    public Transform gatilho;
    public GameObject ProjetilPreFab;
    public float velocidadeDoProjetil = 10;
    public float distanciaMaximaDoProjetil = 10;
    float tempoDeVidaDoProjetil;
    public int danoCausado = 5;
    public float frequenciaDoTiro;
    [System.NonSerialized] public float timer = 0;

    public int vidaMaxima = 50;
    public int vidaAtual;
    public BarraDeVidaScript barraDeVida;
    public int danoSofrido;
    ControleDosInimgiosScript controleDeInimigo;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        controleDeInimigo = GameObject.Find("Inimigos").GetComponent<ControleDosInimgiosScript>();
        AI = GetComponent<NavMeshAgent>();

        vidaAtual = vidaMaxima;
        barraDeVida.AlterarBarraDeVida(vidaAtual, vidaMaxima);
    }

    void Update()
    {
        distanciaPlayerBase = Vector3.Distance(eixo.position, player.position);
        distanciaPlayerEnemy = Vector3.Distance(transform.position, player.position);

        if (distanciaPlayerBase <= raioDeGuarda && distanciaPlayerEnemy > distanciaDeAtaque)
        {
            Perseguir();
        }
        else if (distanciaPlayerEnemy <= distanciaDeAtaque)
        {
            Atacar();
        }
        else
        {
            Patrulhar();
        }
    }
    void Patrulhar()
    {
        AI.isStopped = false;
        AI.stoppingDistance = 0;
        AI.speed = velocidadeAoAndar;
        if (AI.remainingDistance < 0.1)
        {
            if (index >= wayPoints.Length - 1)
            {
                index = 0;
            }
            else
            {
                index++;
            }
            AI.SetDestination(wayPoints[index].position);
        }
    }
    void Perseguir()
    {
        AI.isStopped = false;
        AI.stoppingDistance = distanciaDeAtaque;
        AI.speed = velocidadeAoCorrer;
        AI.SetDestination(player.position);
    }
    void Atacar()
    {
        AI.isStopped = true;
        Rotacionar();
        Atirar();
    }
    void Atirar()
    {
        if (timer > frequenciaDoTiro)
        {
            GameObject bala = Instantiate(ProjetilPreFab, gatilho.position, gatilho.rotation);
            bala.GetComponent<Rigidbody>().velocity = gatilho.forward * velocidadeDoProjetil;

            timer = 0;

            tempoDeVidaDoProjetil = distanciaMaximaDoProjetil / velocidadeDoProjetil;
            Destroy(bala, tempoDeVidaDoProjetil);
        }
        timer += Time.deltaTime;
    }
    void Rotacionar()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, velocidadeDeGiro * Time.deltaTime);
    }
    public void AplicarDanoNoInimigo(int dano)
    {
        dano = danoSofrido;
        vidaAtual -= dano;
        barraDeVida.AlterarBarraDeVida(vidaAtual, vidaMaxima);

        if (vidaAtual <= 0)
        {
            gameObject.SetActive(false);
            controleDeInimigo.numeroDeInimigosLiquidados += 1;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(eixo.position, raioDeGuarda);
        if(distanciaPlayerEnemy <= raioDeGuarda)
        {
            Gizmos.DrawWireSphere(transform.position, distanciaDeAtaque);
        }
        
    }
}
