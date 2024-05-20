using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.AI;

public class AgentesScript : MonoBehaviour
{
    [System.NonSerialized]
    public NavMeshAgent AI;

    [System.NonSerialized]
    public ControleDeFaseScript controleDeFase;

    public Transform origem;

    //[System.NonSerialized]
    public GameObject player;
   // [System.NonSerialized]
    public PlayerScript playerScript;
    float distanciaPlayerAgente;
    public float distanciaLimiteAgentePlayer = 5;

    public float velocidadeDeGiro = 50;
    public float velocidadeDoAgente;
    public bool olharParaOPlayer;

    public Transform gatilho;
    public GameObject ataquePreFab;
    public float velocidadeDoAtaque = 20;
    public float alcanceDoAtaque = 10;
    float sobrevidaDoAtaque;
    public float distanciaMinimaParaAtacar = 10;

    public float frequenciaDoTiro = 1;
    float timer = 0;
    public int vida = 1;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();
        controleDeFase = GameObject.Find("ControleDeFaseBoss").GetComponent<ControleDeFaseScript>();

        AI = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        distanciaPlayerAgente = Vector3.Distance(transform.position, player.transform.position);

        Perseguir();
        GerenciarVida();
        OlharPara();

        if (distanciaPlayerAgente <= distanciaMinimaParaAtacar)
        {
            Atirar();
        }
        if (AI.isStopped)
        {
            Rotacionar();
        }
    }
    void Perseguir()
    {
        AI.stoppingDistance = distanciaLimiteAgentePlayer;
        AI.speed = velocidadeDoAgente;
        AI.SetDestination(player.transform.position);
    }
    void Rotacionar()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
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
    }
    void GerenciarVida()
    {
        if (vida <= 0)
        {
            controleDeFase.numeroDeAgentes -= 1;
            gameObject.SetActive(false);
        }

        if (gameObject.activeSelf == false)
        {
            transform.position = origem.position;
            transform.rotation = origem.rotation;
        }
    }
    void OlharPara()
    {
        if(olharParaOPlayer == true)
        {
            Quaternion anguloDoPlayer = Quaternion.LookRotation(player.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, anguloDoPlayer, velocidadeDeGiro * Time.deltaTime);
        }
        
    }
}
