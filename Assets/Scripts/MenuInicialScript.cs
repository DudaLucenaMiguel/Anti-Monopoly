using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicialScript : MonoBehaviour
{
    public GameObject painelInformativo;

    void Start()
    {
        painelInformativo.SetActive(false);
    }

    void Update()
    {
        Teclado();
    }
    void Teclado()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            AbrirPainelInformativo();
        }
    }
    public void AbrirPainelInformativo()
    {
        if(!painelInformativo.activeSelf)
        {
            painelInformativo.SetActive(true);
        }
        else
        {
            painelInformativo.SetActive(false);
        }
        
    }
    public void ProximaScena(string SceneDestino)
    {
        SceneManager.LoadScene(SceneDestino);
    }
}
