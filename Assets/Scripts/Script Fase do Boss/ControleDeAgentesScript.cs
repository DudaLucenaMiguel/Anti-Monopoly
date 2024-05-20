using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleDeAgentesScript : MonoBehaviour
{
    [System.NonSerialized]
    public GameObject[] agentes;
    [System.NonSerialized]
    public AgentesScript[] agentesScript;

    public float velocidadeDeGiro_ = 50;
    public float velocidadeDoAgente_ = 5;
    public float velocidadeDoAtaque_ = 20;
    public float alcanceDoAtaque_ = 10;
    public float distanciaMinimaParaAtacar_ = 10;
    public float frequenciaDoTiro_ = 3;
    public bool mirarNoPlayer;
    private void Awake()
    {
        agentes = GameObject.FindGameObjectsWithTag("Agente");
        agentesScript = new AgentesScript[agentes.Length];
        for (int i = 0; i < agentes.Length; i++)
        {
            agentesScript[i] = agentes[i].GetComponent<AgentesScript>();
        }
    }
    
    void Update()
    {
        for(int i = 0; i < agentesScript.Length; i++)
        {
            agentesScript[i].velocidadeDeGiro = velocidadeDeGiro_;
            agentesScript[i].velocidadeDoAgente = velocidadeDoAgente_;
            agentesScript[i].velocidadeDoAtaque = velocidadeDoAtaque_;
            agentesScript[i].alcanceDoAtaque = alcanceDoAtaque_;
            agentesScript[i].distanciaMinimaParaAtacar = distanciaMinimaParaAtacar_;
            agentesScript[i].frequenciaDoTiro = frequenciaDoTiro_;
            agentesScript[i].olharParaOPlayer = mirarNoPlayer;
            
        }
    }
}
