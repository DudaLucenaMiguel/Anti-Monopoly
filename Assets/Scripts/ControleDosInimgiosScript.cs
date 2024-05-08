using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleDosInimgiosScript : MonoBehaviour
{
    
    [Range(0, 20)] public float _raiosDeGuarda;
    [Range(0, 10)] public float _distanciaDeAtaque;
    public int _frequenciaDoTiro, _danoCausado, _vidaMaxima;
    public GameObject[] inimigos;
    public InimgoScript[] inimigosScripts;

    void Start()
    {
        inimigos = GameObject.FindGameObjectsWithTag("Inimigo");
        inimigosScripts = new InimgoScript[inimigos.Length];
        for(int i = 0; i<inimigosScripts.Length; i++)
        {
            inimigosScripts[i] = inimigos[i].GetComponent<InimgoScript>();
        }
    }
    void Update()
    {
        for(int i = 0; i<inimigosScripts.Length; i++)
        {
            inimigosScripts[i].raioDeGuarda = _raiosDeGuarda;
            inimigosScripts[i].distanciaDeAtaque = _distanciaDeAtaque;
            inimigosScripts[i].frequenciaDoTiro = _frequenciaDoTiro;
            inimigosScripts[i].danoCausado = _danoCausado;
            inimigosScripts[i].vidaMaxima = _vidaMaxima;
        }
    }
}
