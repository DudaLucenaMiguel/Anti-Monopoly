using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicialScript : MonoBehaviour
{
    public GameObject painelInformativo;

    public GameObject[] imagens;

   
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
        if (Input.GetKeyDown(KeyCode.I))
        {
            AbrirPainelInformativo();
        }
        if(Input.GetKey(KeyCode.F) && Input.GetKey(KeyCode.Keypad1) && SceneManager.GetActiveScene().name != "Fase 1")
        {
            ProximaScena("Fase 1");
        }
        if (Input.GetKey(KeyCode.F) && Input.GetKey(KeyCode.Keypad2) && SceneManager.GetActiveScene().name != "Fase 2")
        {
            ProximaScena("Fase 2");
        }
        if (Input.GetKey(KeyCode.F) && Input.GetKey(KeyCode.Keypad3) && SceneManager.GetActiveScene().name != "Fase 3")
        {
            ProximaScena("Fase 3");
        }
        if (Input.GetKey(KeyCode.B) && Input.GetKey(KeyCode.Keypad1) && SceneManager.GetActiveScene().name != "Boss 1")
        {
            ProximaScena("Boss 1");
        }
        if (Input.GetKey(KeyCode.B) && Input.GetKey(KeyCode.Keypad2) && SceneManager.GetActiveScene().name != "Boss 2")
        {
            ProximaScena("Boss 2");
        }
        if (Input.GetKey(KeyCode.B) && Input.GetKey(KeyCode.Keypad3) && SceneManager.GetActiveScene().name != "Boss 3")
        {
            ProximaScena("Boss 3");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
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
