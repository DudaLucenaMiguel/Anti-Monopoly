using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_PlayerScript : MonoBehaviour
{
    //variaveis de movimento
    CharacterController CC;
    public float velocidadeDoPlayer = 10;
    Vector3 direcao;
    float smoothTime = 0.05f;
    float currentVelocity;

    //variaveis de tiro
    public Transform gatilho;
    public GameObject projetilPreFab;
    public float velocidadeDoProjetil = 20;
    public float distanciaMaximaDoProjetil;
    float tempoDeVidaDoProjetil;
    public int danoCausado = 1;
    public float frequenciaDeTiro = 0;
    [System.NonSerialized] public float timer;

    //variaveis de vida
    public int vidaMaxima = 100;
    public int vidaAtual;
    public int danoSofrido;
    public BossScript boss;


    void Start()
    {
        CC = GetComponent<CharacterController>();
        boss = GameObject.Find("Boss").GetComponent<BossScript>();
        vidaAtual = vidaMaxima;
    }

    void Update()
    {
        boss.danoSofrido = danoCausado;
        Movimentar();
        Rotacionar();
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
        
        if (vidaAtual <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
