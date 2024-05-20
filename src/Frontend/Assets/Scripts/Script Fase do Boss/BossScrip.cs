using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScrip : MonoBehaviour
{
    public float precisao;
    public Transform gatilho;
    public GameObject ataquePreFab;
    public float velocidadeDoAtaque;
    public float timerDoTiro;
    public float frequenciaDoTiro;

    public bool bossAtingido;
    public bool atirar;

    public int vidaMaxima = 4;
    public int vidaAtual;
    public BarraDeVidaScript barraDeVida;

    [System.NonSerialized]
    public GameObject player;
    public Transform corpoDoBoss;

    private void Awake()
    {
        player = GameObject.Find("CorpoDoPlayer");
    }
    void Start()
    {
        vidaAtual = vidaMaxima;
        barraDeVida.AlterarBarraDeVida(vidaAtual, vidaMaxima);
    }
    void Update()
    {
        OlharParaOPlayer();
        GerenciarVida();

        if (atirar)
        {
            BossAtacar();
        }
    }
    void OlharParaOPlayer()
    {
        Quaternion anguloDoPlayer = Quaternion.LookRotation(player.transform.position - corpoDoBoss.position).normalized;
        corpoDoBoss.rotation = Quaternion.RotateTowards(corpoDoBoss.rotation, anguloDoPlayer, precisao * Time.deltaTime);
    }
    void BossAtacar()
    {
        if (timerDoTiro > frequenciaDoTiro)
        {
            GameObject ataque = Instantiate(ataquePreFab, gatilho.position, gatilho.rotation);
            ataque.GetComponent<Rigidbody>().velocity = gatilho.forward * velocidadeDoAtaque;

            timerDoTiro = 0;
        }
        timerDoTiro += Time.deltaTime;
    }
    void GerenciarVida()
    {
        barraDeVida.AlterarBarraDeVida(vidaAtual, vidaMaxima);

        if (vidaAtual <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
