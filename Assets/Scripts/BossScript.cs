using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class BossScript : MonoBehaviour
{
    public float Velocidade;

    [System.NonSerialized] public Transform player;
    public Transform corpoDoBoss;

    public int vidaMaxima = 4;
    public int vidaAtual;
    public BarraDeVidaScript barraDeVida;
    public int danoSofrido;

    [System.NonSerialized] public GameObject[] spawnDosAgentes;
    public GameObject agentePreFab;
    public int numeroDeAgentes;

    [System.NonSerialized] public GameObject[] barreiras;
    [System.NonSerialized] public BarreiraScript[] barreirasScript;
    public int numeroDeBarreiras;

    [System.NonSerialized] public ControleDeScene controleDeScene;
    
    public Transform gatilho;
    public GameObject ataquePreFab;
    public float velocidadeDoAtaque;
    public float timerDoTiro;
    public float frequenciaDoTiro;
    public float tempoDeAtaqueDoBoss;
    public float timerDoAtaque;

    public bool bossReage = false;
    public bool convocarAgentes = false;
    public bool convocarBarreiras = false;
    public bool bossAtaca = false;

    private void Awake()
    {
        CaptarBarreiras();
        CaptarAgentes();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        controleDeScene = GameObject.Find("ControleDeScenes").GetComponent<ControleDeScene>();
    }
    void Start()
    {
        vidaAtual = vidaMaxima;
        barraDeVida.AlterarBarraDeVida(vidaAtual, vidaMaxima);
        danoSofrido = 1;
    }
    void Update()
    {
       OlharParaOPlayer();
       GerenciarReacaoDoBoss();
    }
    
    void OlharParaOPlayer()
    {
        Quaternion anguloDoPlayer = Quaternion.LookRotation(player.position - corpoDoBoss.position).normalized;
        corpoDoBoss.rotation = Quaternion.RotateTowards(corpoDoBoss.rotation, anguloDoPlayer, Velocidade * Time.deltaTime);
    }
    
    void GerenciarReacaoDoBoss()
    {
        if(bossReage)
        {
            numeroDeAgentes = spawnDosAgentes.Length;
            numeroDeBarreiras = barreiras.Length;
            timerDoAtaque = tempoDeAtaqueDoBoss;

            convocarAgentes = true;
            convocarBarreiras = true;
            bossAtaca = true;

            bossReage = false;
        }
        
        if (convocarAgentes)
        {
            InvocarAgentes();
            convocarAgentes = false;
        }
        if(convocarBarreiras)
        {
            InvocarBarreiras();
            convocarBarreiras = false;
        }

        if (bossAtaca)
        {
            danoSofrido = 0;
        }
        else
        {
            danoSofrido = 1;
        }

        if (bossAtaca == true && numeroDeAgentes == 0)
        {
            for (int i = 0; i < barreirasScript.Length; i++)
            {
                barreirasScript[i].danoSofrido = 1;
            }
        }
        else
        {
            for (int i = 0; i < barreirasScript.Length; i++)
            {
                barreirasScript[i].danoSofrido = 0;
            }
        }
        
        if(bossAtaca == true && numeroDeAgentes == 0 && numeroDeBarreiras == 0)
        {
            if (timerDoAtaque >= 0)
            {
                timerDoAtaque -= Time.deltaTime;
                BossAtacar();
            }
            else if (timerDoAtaque <= 0)
            {
                bossAtaca = false;
            }
        }
    }
    void CaptarAgentes()
    {
        spawnDosAgentes = GameObject.FindGameObjectsWithTag("SpawnDeAgentes");
        numeroDeAgentes = spawnDosAgentes.Length;
    }
    void InvocarAgentes()
    {
        for (int i = 0; i < spawnDosAgentes.Length; i++)
        {
            GameObject agente = Instantiate(agentePreFab, spawnDosAgentes[i].transform.position, spawnDosAgentes[i].transform.rotation);
        }    
    }
    void CaptarBarreiras()
    {
        barreiras = GameObject.FindGameObjectsWithTag("Barreira");
        barreirasScript = new BarreiraScript[barreiras.Length];
        for (int i = 0; i < barreiras.Length; i++)
        {
            barreirasScript[i] = barreiras[i].GetComponent<BarreiraScript>();
        }
        numeroDeBarreiras = barreiras.Length;
    }
    void InvocarBarreiras()
    {
        for (int i = 0; i<barreiras.Length; i++)
        {
            barreiras[i].SetActive(true);
        }
    }
    void BossAtacar()
    {
        if(timerDoTiro > frequenciaDoTiro)
        {
            GameObject ataque = Instantiate(ataquePreFab, gatilho.position, gatilho.rotation);
            ataque.GetComponent<Rigidbody>().velocity = gatilho.forward * velocidadeDoAtaque;

            timerDoTiro = 0;
        }
        timerDoTiro += Time.deltaTime;
    }
    public void AplicarDanoNoBoss(int dano)
    {
        dano = danoSofrido;
        vidaAtual -= dano;
        barraDeVida.AlterarBarraDeVida(vidaAtual, vidaMaxima);
        if(!bossAtaca)
        {
            bossReage = true;
        }
        if (vidaAtual <= 0)
        {
            gameObject.SetActive(false);
            controleDeScene.venceu = true;
        }
    }
}
