using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjetilDoInimigoScript : MonoBehaviour
{
    
    PlayerScript player;
    int dano;

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        player = collision.transform.GetComponent<PlayerScript>();
        AplicarDano();
    }
    void AplicarDano()
    {
        if (player != null)
        {
            player.AplicarDanoNoPlayer(dano);
        }

    }
}
