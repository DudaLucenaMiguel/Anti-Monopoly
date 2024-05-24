using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour
{

    //variaveis de movimento
    CharacterController CC;
    public float velocidadeDoPlayer = 10;
    Vector3 direcao;
    public float velocidadeDeGiro = 200;
    float smoothTime = 0.05f;
    float currentVelocity;
    float valorDaGravidade = -9.81f;
    Vector3 gravidade;

    public GameObject corpoDoPlayer;

    //variaveis de tiro
    public Transform gatilho;
    public GameObject projetilPreFab;
    public float velocidadeDoProjetil = 20;
    public float distanciaMaximaDoProjetil = 10;
    float tempoDeVidaDoProjetil;
    public int danoCausado;
    public float frequenciaDeTiro = 0;
    [System.NonSerialized] public float timer;
    public BarraDeTiroScript barraDeTiro;
    public LineRenderer linhaDeFogo;
    public Transform Base;
    Vector3 alvo;

    //variaveis de vida
    public int vidaMaxima = 100;
    public int vidaAtual;
    public int danoSofrido;
    public BarraDeVidaScript barraDeVida;

    public Transform origem;
    public bool voltarAOrigem;

    private void Awake()
    {
        Base = GameObject.Find("Base").transform;
        CC = GetComponent<CharacterController>();
        origem = GameObject.Find("OrigemDoPlayer").transform;
        linhaDeFogo = GameObject.Find("LinhaDeFogo").GetComponent<LineRenderer>();
    }
    void Start()
    {
        vidaAtual = vidaMaxima;
        barraDeVida.AlterarBarraDeVida(vidaAtual, vidaMaxima);
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Girar();
        }
        else
        {
            Rotacionar();
            Movimentar();
        }
        Atirar();
        GerenciarVida();
        ResetarPosicao();
        Mirar();

    }
    public void Movimentar()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        direcao = new Vector3(horizontal, 0, vertical);
        CC.Move(direcao * velocidadeDoPlayer * Time.deltaTime);

        gravidade = new Vector3(0, valorDaGravidade, 0);
        CC.Move(gravidade * Time.deltaTime);
    }
    public void Rotacionar()
    {
        if (direcao.magnitude >= smoothTime)
        {
            var targetAngle = Mathf.Atan2(direcao.x, direcao.z) * Mathf.Rad2Deg;
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }
    void Girar()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.Rotate(Vector3.up, horizontalInput * velocidadeDeGiro * Time.deltaTime);
    }
    void Mirar()
    {
        alvo = Base.position + Base.forward * distanciaMaximaDoProjetil;
        linhaDeFogo.positionCount = 2;
        linhaDeFogo.SetPosition(0, Base.transform.position);
        linhaDeFogo.SetPosition(1, alvo);

        linhaDeFogo.alignment = LineAlignment.View;

        if (Input.GetKey(KeyCode.Space))
        {
            linhaDeFogo.enabled = true;
        }
        else
        {
            linhaDeFogo.enabled = false;
        }
    }
    void Atirar()
    {
        if (timer > frequenciaDeTiro)
        {
            if (Input.GetKeyUp(KeyCode.Space))
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

        barraDeTiro.AlterarBarraDeAtaque(timer, frequenciaDeTiro);
    }
    public void AplicarDanoNoPlayer(int dano)
    { 
        dano = danoSofrido;
        vidaAtual -= dano;
    }
    void GerenciarVida()
    {
        if (vidaAtual <= 0)
        {
            gameObject.SetActive(false);
        }

        barraDeVida.AlterarBarraDeVida(vidaAtual, vidaMaxima);
    }

    void ResetarPosicao()
    {
        if(voltarAOrigem == true)
        {
            gameObject.transform.position = origem.transform.position;
            gameObject.transform.rotation = origem.transform.rotation;
            voltarAOrigem = false;
        }
        
    }
    public void AlcanceMaximo(bool ativarAlcanceMaximo)
    {
        Vector3 aFrente = gatilho.position + gatilho.forward * 300;
        RaycastHit hitInfo;
        bool hasHit = Physics.Raycast(gatilho.position, aFrente, out hitInfo, Mathf.Infinity);

        if(hitInfo.distance < distanciaMaximaDoProjetil && ativarAlcanceMaximo == false)
        {
            distanciaMaximaDoProjetil = hitInfo.distance;
        }
        else
        {
            distanciaMaximaDoProjetil = 10;
        }

        if (hasHit && ativarAlcanceMaximo == true)
        {
                distanciaMaximaDoProjetil = hitInfo.distance;
                Debug.DrawRay(gatilho.position, hitInfo.point, Color.red);
        }
        
    }
    
}
