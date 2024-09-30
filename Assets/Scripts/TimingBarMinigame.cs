using UnityEngine;
using UnityEngine.UI;

public class TimingBarMinigame : MonoBehaviour
{
    public RectTransform indicator; // Reference to the indicator UI element
    public RectTransform greenArea; // Reference to the green area
    public float speed = 300f; // Speed of the indicator
    private bool movingRight = true; // Direction of movement
    private float minX, maxX; // Boundaries of the indicator movement

    private void Start()
    {
        // Calculate boundaries based on the width of the panel
        minX = -((RectTransform)transform).rect.width / 2;
        maxX = ((RectTransform)transform).rect.width / 2;
    }

    private void Update()
    {
        // Move the indicator left and right
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

        // Check for spacebar press
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Check if the indicator is within the green area
            if (IsInGreenArea())
            {
                Debug.Log("Success!");
                // Perform success action, such as increasing score or completing a task
            }
            else
            {
                Debug.Log("Missed!");
                // Perform failure action, such as reducing points or restarting
            }
        }
    }

    private bool IsInGreenArea()
    {
        // Check if the indicator is inside the green area bounds
        float indicatorLeft = indicator.anchoredPosition.x - indicator.rect.width / 2;
        float indicatorRight = indicator.anchoredPosition.x + indicator.rect.width / 2;

        float greenLeft = greenArea.anchoredPosition.x - greenArea.rect.width / 2;
        float greenRight = greenArea.anchoredPosition.x + greenArea.rect.width / 2;

        return indicatorRight >= greenLeft && indicatorLeft <= greenRight;
    }
}
