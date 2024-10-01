using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultUIController : MonoBehaviour
{
    public GameObject resultPanel; // Painel que contém o texto e o botão de retry
    public Text resultText;
    public Button retryButton;

    private void Start()
    {
        // Inicialmente, esconde a mensagem de vitória
        resultPanel.SetActive(false);

        // Adiciona o método Retry como o evento do botão de retry
        retryButton.onClick.AddListener(RetryGame);
    }

    // Método para chamar quando o jogador vencer
    public void ShowVictoryScreen()
    {
        resultPanel.SetActive(true);
        resultText.text = "Você venceu! Miquinho conseguiu pagar sua dívida! :D";
    }

    // Método para chamar quando o jogador vencer
    public void ShowLossScreen()
    {
        resultPanel.SetActive(true);
        resultText.text = "Você perdeu! Miquinho foi devorado... D:";
        
    }

    // Método para reiniciar o jogo
    public void RetryGame()
    {
        // Reinicia a cena atual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
