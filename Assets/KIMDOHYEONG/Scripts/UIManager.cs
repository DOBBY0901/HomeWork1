using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Text")]
    [SerializeField] private TextMeshProUGUI timeText;        // 생존 시간을 표시할 텍스트
    [SerializeField] private TextMeshProUGUI scoreText;       // 점수를 표시할 텍스트

    private void Update()
    {
        // 방어코드 
        if (GameManager.Instance == null) return;

        // 생존 시간을 소수점 첫째 자리까지 표시.
        timeText.text = "Time : " + GameManager.Instance.SurvivalTime.ToString("F1");

        // 현재 점수 표시.
        scoreText.text = "Score : " + GameManager.Instance.Score;
    }
}