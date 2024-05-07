using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueDoPlayerScript : MonoBehaviour
{
    InimgoScript inimigo;
    int dano;

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        inimigo = collision.transform.GetComponent<InimgoScript>();
        AplicarDano();
    }
    void AplicarDano()
    {
        if (inimigo != null)
        {
            inimigo.AplicarDanoNoInimigo(dano);
        }
    }
}
