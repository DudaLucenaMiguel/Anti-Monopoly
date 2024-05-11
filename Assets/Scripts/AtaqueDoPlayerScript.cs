using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueDoPlayerScript : MonoBehaviour
{
    InimgoScript inimigo;
    AgenteScript agente;
    BossScript boss;
    BarreiraScript barreira;
    int dano;

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);

        inimigo = collision.transform.GetComponent<InimgoScript>();
        agente = collision.transform.GetComponent<AgenteScript>();
        boss = collision.transform.GetComponent<BossScript>();
        barreira = collision.transform.GetComponent<BarreiraScript>();

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
            boss.AplicarDanoNoBoss(dano);
        }
        if(barreira != null)
        {
            barreira.AplicarDanoNaBarreira(dano);
        }
    }
}
