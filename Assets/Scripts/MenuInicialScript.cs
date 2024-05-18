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
        if(Input.GetKeyDown(KeyCode.Keypad1) && SceneManager.GetActiveScene().name != "Fase 1")
        {
            ProximaScena("Fase 1");
        }
        if (Input.GetKeyDown(KeyCode.Keypad2) && SceneManager.GetActiveScene().name != "Fase 2")
        {
            ProximaScena("Fase 2");
        }
        if (Input.GetKeyDown(KeyCode.Keypad3) && SceneManager.GetActiveScene().name != "Fase 3")
        {
            ProximaScena("Fase 3");
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
