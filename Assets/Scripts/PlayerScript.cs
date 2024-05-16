using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    //variaveis de movimento
    CharacterController CC;
    public float velocidadeDoPlayer = 10;
    Vector3 direcao;
    public float velocidadeDeGiro = 200;
    float smoothTime = 0.05f;
    float currentVelocity;

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
    public int danoSofrido;
    public BarraDeVidaScript barraDeVida;
    public bool morreu = false;

   

    private void Awake()
    {
        CC = GetComponent<CharacterController>(); 
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
            Mirar();
        }
        else
        {
            Rotacionar();
            Movimentar();
        }
        Atirar();

    }
    public void Movimentar()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        direcao = new Vector3(horizontal, 0, vertical);
        CC.Move(direcao * velocidadeDoPlayer * Time.deltaTime);
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
    void Mirar()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.Rotate(Vector3.up, horizontalInput * velocidadeDeGiro * Time.deltaTime);
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
    }
    public void AplicarDanoNoPlayer(int dano)
    {
        dano = danoSofrido;
        vidaAtual -= dano;
        barraDeVida.AlterarBarraDeVida(vidaAtual, vidaMaxima);

        if (vidaAtual <= 0)
        {
            gameObject.SetActive(false);
            morreu = true;
            
        }
    }
}
