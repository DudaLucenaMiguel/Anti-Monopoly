using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PojetilDoBoss : MonoBehaviour
{
    ControleDeFaseScript controleDeFase;
    PlayerScript player;

    private void Awake()
    {
        controleDeFase = GameObject.Find("ControleDeFaseBoss").GetComponent<ControleDeFaseScript>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        Destroy(gameObject);
        Debug.Log("Atirou");
        player = collision.transform.GetComponent<PlayerScript>();
        NotificarDano();
    }
     void NotificarDano()
    {
        if(player != null)
        {
            controleDeFase.playerAtingido = true;
        }
        
    }
}
