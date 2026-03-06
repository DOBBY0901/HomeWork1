using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI scoreText;

    void Update()
    {
        if (GameManager.gameManager == null) return;

        timeText.text = "Time : " + GameManager.gameManager.survivalTime.ToString("F1");
        scoreText.text = "Score : " + GameManager.gameManager.score;
    }
}