using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControleDeScene : MonoBehaviour
{ 
    public GameObject painelDeGameOver;
    public GameObject painelDePause;
    public GameObject botaoDePause;

    public bool gameOver;
    public bool venceu;

    public string proximaScene;

    void Start()
    {
        Time.timeScale = 1;
        painelDeGameOver.SetActive(false);
        painelDePause.SetActive(false);

        gameOver = false;
        venceu = false;
    }
    void Update()
    {
        if(gameOver)
        {
            AbrirPainelDeGameOver();
        }
        if(venceu)
        {
            ProximaScena(proximaScene);
        }
        
        Teclado();
    }
    void Teclado()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            ProximaScena(proximaScene);
            PausePlay();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
            PausePlay();
        }
        if (Input.GetKeyDown(KeyCode.P) && !painelDeGameOver.activeSelf)
        {
            AbrirPainelDePause();
        }
    }
    public void ProximaScena(string cena)
    {
        SceneManager.LoadScene(cena);
        PausePlay();
    }
    public void Reset()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    public void Quit()
    {
        ProximaScena("MenuDeFases");
    }
    public void AbrirPainelDePause()
    {
        PausePlay();
        if (!painelDePause.activeSelf)
        {
            painelDePause.SetActive(true);
        }
        else
        {
            painelDePause.SetActive(false);
        }
    }
    public void AbrirPainelDeGameOver()
    {
        PausePlay();
        painelDeGameOver.SetActive(true);

        if (painelDeGameOver.activeSelf)
        {
            botaoDePause.SetActive(false);
        }
        else
        {
            botaoDePause.SetActive(true);
        }
    }
    void PausePlay()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
