using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicialScript : MonoBehaviour
{
    public GameObject painelInicial;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void Play()
    {
        painelInicial.SetActive(false);
    }
    public void ProximaScena(string SceneDestino)
    {
        SceneManager.LoadScene(SceneDestino);
    }
}
