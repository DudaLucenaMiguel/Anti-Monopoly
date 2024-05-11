using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarreiraScript : MonoBehaviour
{
    BossScript bossScript;

    public GameObject[] tamanhoDaBarreira;
    public int vida;
    public int danoSofrido;

    private void Start()
    {
        bossScript = GameObject.Find("Boss").GetComponent<BossScript>();
        gameObject.SetActive(false);
        vida = tamanhoDaBarreira.Length;
    }
    void Update()
    {
        AlterarTamanhoDaBarreira();
    }
    void AlterarTamanhoDaBarreira()
    {
        if(vida == 3)
        {
            tamanhoDaBarreira[0].SetActive(true);
            tamanhoDaBarreira[1].SetActive(false);
            tamanhoDaBarreira[2].SetActive(false);
        }
        else if(vida == 2)
        {
            tamanhoDaBarreira[0].SetActive(false);
            tamanhoDaBarreira[1].SetActive(true);
            tamanhoDaBarreira[2].SetActive(false);
        }
        else if(vida == 1)
        {
            tamanhoDaBarreira[0].SetActive(false);
            tamanhoDaBarreira[1].SetActive(false);
            tamanhoDaBarreira[2].SetActive(true);
        }

        if (vida <= 0)
        {
            gameObject.SetActive(false);
            vida = tamanhoDaBarreira.Length;
            bossScript.numeroDeBarreiras -= 1;
        }
    }
    public void AplicarDanoNaBarreira(int dano)
    {
        dano = danoSofrido;
        vida -= dano;
    }
    private void OnCollisionEnter(Collision collision)
    {
        //index++;
    }
}
