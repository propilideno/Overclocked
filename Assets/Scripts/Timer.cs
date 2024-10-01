using UnityEngine;
using UnityEngine.UI; // Se você quiser exibir o tempo em um texto UI

public class Timer : MonoBehaviour
{
    [SerializeField] public float timeRemaining = 5f;  // Defina o tempo inicial (em segundos)
    public bool timerIsRunning = false; // Verifica se o timer está rodando
    public Text timeText;               // Referência ao texto UI para exibir o tempo
    public ResultUIController result;   // Referência ao script que controla a tela de resultado
    public Player player;               // Referência ao script do jogador
    void Start()
    {
        // Inicia o timer
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                // Quando o tempo acabar, execute a lógica necessária
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                if(player.dollarsEarned >= 1000){
                    player.gameInput.disableMoviment();

                    result.ShowVictoryScreen(); // Chama o método para exibir a tela de vitória
                } else {
                    player.gameInput.disableMoviment();

                    result.ShowLossScreen(); // Chama o método para exibir a tela de derrota
                }
            }
        }
    }

    // Função para exibir o tempo formatado no UI
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1; // Adiciona 1 segundo para que o tempo seja exibido corretamente

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);  // Converte para minutos
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);  // Converte para segundos

        timeText.text = "Tempo: " + string.Format("{0:00}:{1:00}", minutes, seconds);  // Atualiza o texto com o formato mm:ss
    }

    
}
