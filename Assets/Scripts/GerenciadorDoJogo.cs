using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorDoJogo : MonoBehaviour
{ 
    public GameObject player;
    public ControleDosInimgiosScript controleDosInimgios;

    public int numeroDeInimigosLiquidados;
    public int numeroDeInimigosTotais;
    public TextMeshProUGUI contadorDeInimigos;

    void Start()
    {
        controleDosInimgios = GameObject.Find("Inimigos").GetComponent<ControleDosInimgiosScript>();
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        GerenciarJogo();
    }
    
    void GerenciarJogo()
    {
        numeroDeInimigosTotais = controleDosInimgios.numeroDeInimigosTotais;
        numeroDeInimigosLiquidados = controleDosInimgios.numeroDeInimigosLiquidados;
        contadorDeInimigos.text = $"Número de inimigos convertidos: {numeroDeInimigosLiquidados}/{numeroDeInimigosTotais}";

        if (numeroDeInimigosLiquidados == numeroDeInimigosTotais)
        {
            GerenciarScena("MapaDoBoss");
        }
        if(!player.activeSelf)
        {
            GerenciarScena("MenuDeFases");
        }
        
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            GerenciarScena("MapaDoBoss");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GerenciarScena("MenuDeFases");
        }

    }
    void GerenciarScena(string cena)
    {
        SceneManager.LoadScene(cena);

        
    }
}
