using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BarraDeTiroScript : MonoBehaviour
{
    public Image ataque; 
    
    public void AlterarBarraDeAtaque(float timer, float frequencia)
    {
        ataque.fillAmount = timer/frequencia;
    }

}
