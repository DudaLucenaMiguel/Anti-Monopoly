using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControleDeScene : MonoBehaviour
{
    public PlayerScript playerScript;

    public GameObject painelDeGameOver;
    public GameObject painelDePause;
    public GameObject botaoDePause;

    public bool gameOver;
    public bool venceu;

    public string proximaScene;

    private void Awake()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
    }
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
        GerenciarJogo();
        Teclado();
    }
    void Teclado()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
            PausePlay();
        }
        if (Input.GetKeyDown(KeyCode.P) && !painelDeGameOver.activeSelf)
        {
            AbrirPainelDePause();
        }
        if(painelDePause.activeSelf || painelDeGameOver.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                Quit();
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad1) && SceneManager.GetActiveScene().name != "Fase 1")
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
    void GerenciarJogo()
    {
        if(playerScript.morreu == true)
        {
            gameOver = true;
        }

        if (gameOver)
        {
            AbrirPainelDeGameOver();
        }

        if (venceu)
        {
            ProximaScena(proximaScene);
        }

    }
}
