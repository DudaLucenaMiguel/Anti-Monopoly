using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenhascoScript : MonoBehaviour
{
    public PlayerScript playerCollision;

    public ControleDeFaseScript controleDeFase;
    private void Awake()
    {
        controleDeFase = GameObject.Find("ControleDeFaseBoss").GetComponent<ControleDeFaseScript>();
    }
    private void OnTriggerEnter(Collider other)
    {
        playerCollision = other.transform.GetComponent<PlayerScript>();

        if (playerCollision != null)
        {
            controleDeFase.caiuDoPesnhasco = true;
        }
        
    }

    
}
