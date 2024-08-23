using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {

    public Text scoreText;  // Arraste o ScoreText do Unity aqui
    private Player player;  // Referência ao script Player para acessar os pontos
    
    void Start()
    {
        player = FindObjectOfType<Player>();  // Encontra o Player na cena
        UpdateScoreText();  // Atualiza o texto no início
    }

    void Update()
    {
        UpdateScoreText();  // Atualiza o texto em cada frame
    }

    void UpdateScoreText()
    {
        scoreText.text = "Dindin: " + player.GetDollars().ToString();  
    }
}
