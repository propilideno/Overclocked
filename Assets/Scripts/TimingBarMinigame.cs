using UnityEngine;
using UnityEngine.UI;

public class TimingBarMinigame : MonoBehaviour
{
    public RectTransform indicator;
    public RectTransform greenArea;
    public float speed = 3f;
    private bool movingRight = true;
    private float minX, maxX;

    // Referência ao script da barra de progresso
    [SerializeField] public FixCounter fixCounter;
    private void Start()
    {
        minX = -((RectTransform)transform).rect.width / 3;
        maxX = ((RectTransform)transform).rect.width / 3;
    }

    private void Update()
    {
        if (movingRight)
        {
            indicator.anchoredPosition += Vector2.right * speed * Time.deltaTime;
            if (indicator.anchoredPosition.x >= maxX)
                movingRight = false;
        }
        else
        {
            indicator.anchoredPosition += Vector2.left * speed * Time.deltaTime;
            if (indicator.anchoredPosition.x <= minX)
                movingRight = true;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (IsInGreenArea())
            {
                Debug.Log("Sucesso!");
                // Chame a função para aumentar a barra de progresso
                fixCounter.IncrementFixProgress();
            }
            else
            {
                Debug.Log("Falhou!");
                // Ação de falha
            }
        }
    }

    private bool IsInGreenArea()
    {
        float indicatorLeft = indicator.anchoredPosition.x - indicator.rect.width / 2;
        float indicatorRight = indicator.anchoredPosition.x + indicator.rect.width / 2;

        float greenLeft = greenArea.anchoredPosition.x - greenArea.rect.width / 2;
        float greenRight = greenArea.anchoredPosition.x + greenArea.rect.width / 2;

        return indicatorRight >= greenLeft && indicatorLeft <= greenRight;
    }
}
