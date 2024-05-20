using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVidaScript : MonoBehaviour
{
    public Image vidaRestante;
    public Transform foco;

    private void Awake()
    {
        foco = Camera.main.transform;
    }
    void Update()
    {
        transform.LookAt(transform.position + foco.forward);
    }
    public void AlterarBarraDeVida(int vidaAtual, int vidaMaxima)
    {
        vidaRestante.fillAmount = (float) vidaAtual / vidaMaxima;
    }
}
