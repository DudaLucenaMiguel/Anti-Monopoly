using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueDoPlayerScript : MonoBehaviour
{
    InimgoScript inimigo;
    AgentesScript agente;
    BossScrip boss;
    BarreirasScript barreira;
    int dano;

    public void OnTriggerEnter(Collider collision)
    {
        Destroy(gameObject);

        inimigo = collision.transform.GetComponent<InimgoScript>();
        agente = collision.transform.GetComponent<AgentesScript>();
        boss = collision.transform.GetComponent<BossScrip>();
        barreira = collision.transform.GetComponent<BarreirasScript>();

        AplicarDano();
    }
    void AplicarDano()
    {
        if (inimigo != null)
        {
            inimigo.AplicarDanoNoInimigo(dano);
        }
        if(agente != null)
        {
            agente.AplicarDanoNoAgente(dano);
        }
        if(boss != null)
        {
            ControleDeFaseScript controleDeFase = GameObject.Find("ControleDeFaseBoss").GetComponent<ControleDeFaseScript>();
            controleDeFase.bossAtingido = true;
        }
        if(barreira != null)
        {
            barreira.AplicarDanoNaBarreira(dano);
        }
    }
}
