using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_PlayerScript : MonoBehaviour
{
    //variaveis de movimento
    CharacterController CC;
    public float velocidadeDoPlayer = 10;
    Vector3 direcao;

    //variaveis de tiro
    public Transform gatilho;
    public GameObject projetilPreFab;
    public float velocidadeDoProjetil = 20;
    public float distanciaMaximaDoProjetil = 10;
    float tempoDeVidaDoProjetil;
    public int danoCausado;
    public float frequenciaDeTiro = 0;
    [System.NonSerialized] public float timer;

    //variaveis de vida
    public int vidaMaxima = 100;
    public int vidaAtual;
    public BarraDeVidaScript barraDeVida;
    public int danoSofrido;


    void Start()
    {
        CC = GetComponent<CharacterController>();
    }

    void Update()
    {
        Movimentar();
        Atirar();
    }
    public void Movimentar()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        direcao = new Vector3(horizontal, 0, vertical);
        CC.Move(direcao * velocidadeDoPlayer * Time.deltaTime);
    }
    void Atirar()
    {
        if (timer > frequenciaDeTiro)
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                GameObject ataque = Instantiate(projetilPreFab, gatilho.position, gatilho.rotation);
                Rigidbody rb = ataque.GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * velocidadeDoProjetil, ForceMode.Impulse);

                tempoDeVidaDoProjetil = distanciaMaximaDoProjetil / velocidadeDoProjetil;
                Destroy(ataque, tempoDeVidaDoProjetil);

                timer = 0;
            }
        }
        timer += Time.deltaTime;
    }
    public void AplicarDanoNoPlayer(int dano)
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
