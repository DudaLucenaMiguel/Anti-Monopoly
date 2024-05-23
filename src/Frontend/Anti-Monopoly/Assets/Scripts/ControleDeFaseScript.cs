using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class ControleDeFaseScript : MonoBehaviour
{
    public int quantidadeDeTurnos = 4;

    [System.NonSerialized]
    public GameObject player;
    [System.NonSerialized]
    public PlayerScript playerScript;
    [System.NonSerialized]
    public GameObject boss;
    [System.NonSerialized]
    public BossScrip bossScript;
    [System.NonSerialized]
    public ControleDeScene controleDeScene;

    [System.NonSerialized]
    public GameObject[] agentes;
    [System.NonSerialized]
    public AgentesScript[] agentesScript;
    public int numeroDeAgentes;

    [System.NonSerialized]
    public GameObject[] barreiras;
    [System.NonSerialized]
    public BarreirasScript[] barreirasScript;
    public int numeroDeBarreiras;

    public float tempoPorBarreira = 5;
    public float tempoParaDestruirBarreiras;
    public float timerParaDestruirBarreiras;

    public float tempoDeAtaqueDoBoss = 10;
    public float timerDoAtaqueDoBoss;

    public TextMeshProUGUI timerText;

    public bool ataque;
    public bool playerAtingido;
    public bool bossAtingido;
    public bool bossAtirando;

    public bool caiuDoPesnhasco;

    private void Awake()
    {
        //captar Player
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();

        //captar Boss
        boss = GameObject.Find("Boss");
        bossScript = boss.GetComponent<BossScrip>();
 
        //captar Agentes
        agentes = GameObject.FindGameObjectsWithTag("Agente");
        agentesScript = new AgentesScript[agentes.Length];
        for(int i = 0; i<agentes.Length; i++)
        {
            agentesScript[i] = agentes[i].GetComponent<AgentesScript>();
        }

        //captar Barreiras
        barreiras = GameObject.FindGameObjectsWithTag("Barreira");
        barreirasScript = new BarreirasScript[barreiras.Length];
        for (int i = 0; i < barreiras.Length; i++)
        {
            barreirasScript[i] = barreiras[i].GetComponent<BarreirasScript>();
        }

        //captar Controle de Scene
        controleDeScene = GameObject.Find("ControleDeScenes").GetComponent<ControleDeScene>();

        
    }
    void Start()
    {
        playerScript.danoSofrido = 0;
        //playerScript.distanciaMaximaDoProjetil = 200;
        

        playerScript.vidaMaxima = quantidadeDeTurnos;
        playerScript.vidaAtual = quantidadeDeTurnos;

        bossScript.vidaMaxima = quantidadeDeTurnos;
        bossScript.vidaAtual = quantidadeDeTurnos;

        tempoParaDestruirBarreiras = barreiras.Length * tempoPorBarreira;

        ReiniciarOTurno();
    }
    void Update()
    {
        Reagir();
        GerenciarJogo();
        Caiu();
        playerScript.AlcanceMaximo(true);
    }  
    void InvocarAgentesEBarreiras()
    {
        for (int i = 0; i < barreiras.Length; i++)
        {
            barreiras[i].SetActive(true);
        }

        for (int i = 0; i < agentes.Length; i++)
        {
            agentes[i].SetActive(true);
        }
    }
    void Reagir()
    {
        if (bossAtingido == true && ataque == false)
        {
            bossAtingido = false;
            ataque = true;

            InvocarAgentesEBarreiras();
        }
        else if(bossAtingido == true && ataque == true)
        {
            bossAtingido = false;
        }

        if(ataque)
        {
                if (numeroDeAgentes == 0 && playerAtingido == false && bossAtirando == false)
                {
                    timerParaDestruirBarreiras -= Time.deltaTime;
                    timerText.text = $"{Mathf.FloorToInt(timerParaDestruirBarreiras)}";

                    for (int i = 0; i < barreirasScript.Length; i++)
                    {
                        barreirasScript[i].danoSofrido = 1;
                    }
                }
                else if(numeroDeAgentes != 0 && playerAtingido == false && bossAtirando == false)
                {
                    for (int i = 0; i < barreirasScript.Length; i++)
                    {
                        barreirasScript[i].danoSofrido = 0;
                    }
                }
                else if (numeroDeAgentes != 0 && playerAtingido == true && bossAtirando == false)
                {
                    playerScript.vidaAtual -= 1;
                    ReiniciarOTurno();
                }
            
            if (timerParaDestruirBarreiras >= 0 && numeroDeBarreiras == 0)
            {
                if(timerDoAtaqueDoBoss >= 0 && playerAtingido == false)
                {
                    bossAtirando = true;
                    timerDoAtaqueDoBoss -= Time.deltaTime;
                    timerText.text = $"{Mathf.FloorToInt(timerDoAtaqueDoBoss)}";
                }
                else if(timerDoAtaqueDoBoss >= 0 && playerAtingido == true)
                {
                    playerScript.vidaAtual -= 1;
                    ReiniciarOTurno();
                }
                else if(timerDoAtaqueDoBoss <= 0 && playerAtingido == false)
                {
                    bossScript.vidaAtual -= 1;
                    ReiniciarOTurno();
                }
            }
            else if (timerParaDestruirBarreiras <= 0 && numeroDeBarreiras != 0)
            {
                playerScript.vidaAtual -= 1;
                ReiniciarOTurno();
            }
            
            if(bossAtirando)
            {
                bossScript.atirar = true;
            }
            else
            {
                bossScript.atirar = false;
            }
        }
    }
    void ReiniciarOTurno()
    {
        ataque = false;

        playerAtingido = false;

        bossAtirando = false;
        timerDoAtaqueDoBoss = tempoDeAtaqueDoBoss;

        for (int i = 0; i < agentes.Length; i++)
        {
            agentes[i].transform.position = agentesScript[i].origem.transform.position;
            agentes[i].transform.rotation = agentesScript[i].origem.transform.rotation;
            agentesScript[i].vida = 1;
            agentes[i].SetActive(false);
        }
        numeroDeAgentes = agentes.Length;

        for (int i = 0; i < barreiras.Length; i++)
        {
            barreiras[i].SetActive(false);
            barreirasScript[i].vida = 3;
        }

        numeroDeBarreiras = barreiras.Length;
        timerParaDestruirBarreiras = tempoParaDestruirBarreiras;

        timerText.text = " ";

        playerScript.voltarAOrigem = true;
    }

    void Caiu()
    {
        if(caiuDoPesnhasco == true)
        {
            playerScript.vidaAtual -= 1;
            ReiniciarOTurno();
            caiuDoPesnhasco = false;
        }

        
    }

    void GerenciarJogo()
    {
        if(boss.activeSelf == false)
        {
            controleDeScene.venceu = true;
        }
        else if(player.activeSelf == false)
        {
            controleDeScene.gameOver = true;
        }
    }

}
