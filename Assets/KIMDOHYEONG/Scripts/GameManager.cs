using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOverImage;        // 게임오버 이미지
    [SerializeField] private GameObject restartButton;        // 재시작 버튼

    [Header("Best Score UI")]
    [SerializeField] private TMP_Text bestScoreText;          // 최고 점수

    [Header("Life Settings")]
    [SerializeField] private int life = 3;                    // 플레이어 목숨 수 
    [SerializeField] private Image[] hearts;                  // 플레이어 목숨을 표시할 하트 이미지 배열

    [Header("Score Settings")]
    [SerializeField] private int scorePerSecond = 10;         // 초당 증가할 점수

    private bool isGameOver = false;                          // 현재 게임오버 상태인지 확인
    private int score = 0;                                    // 현재 점수
    private int bestScore = 0;                                // 최고 점수
    private float scoreTimer = 0f;                            // 초당 점수 증가용 타이머
    private float survivalTime = 0f;                          // 현재 생존 시간

    // 프로퍼티
    public bool IsGameOver => isGameOver;
    public int Score => score;
    public float SurvivalTime => survivalTime;
    public int Life => life;

    private void Awake()
    {
        // 싱글톤으로 GameManager 하나만 존재하도록.
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        // 시작 시 게임오버 UI는 꺼두기.
        gameOverImage.SetActive(false);
        restartButton.SetActive(false);

        // 이전 최고 점수를 불러온다. 저장된 값이 없으면 0을 사용한다.
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestScoreText.text = bestScore.ToString();

        // TimeScale 초기화.
        Time.timeScale = 1f;

        // 시작 시 현재 목숨 상태에 맞게 하트 UI를 갱신한다.
        UpdateHeartUI();
    }

    private void Update()
    {
        // 게임오버 상태면 시간과 점수를 더 이상 증가X.
        if (isGameOver) return;

        // 생존 시간 누적.
        survivalTime += Time.deltaTime;

        // 점수 타이머를 누적해서 1초마다 점수를 추가한다.
        scoreTimer += Time.deltaTime;

        if (scoreTimer >= 1f)
        {
            score += scorePerSecond;
            scoreTimer = 0f;
        }
    }

    /// <summary>
    /// 특정 장애물로 점수를 추가할 때 사용한다.
    /// </summary>
    public void AddScore(int value)
    {
        score += value;
    }

    /// <summary>
    /// 플레이어가 장애물에 맞았을 때 호출된다.
    /// 목숨을 1 감소시키고, 0이 되면 게임오버 처리한다.
    /// </summary>
    public void PlayerHit()
    {
        if (isGameOver) return;

        life--;
        UpdateHeartUI();

        if (life <= 0)
        {
            GameOver();
        }
    }

    /// <summary>
    /// 게임을 종료 상태로 바꾸고, 최고 점수를 저장하고, 화면의 장애물과 플레이어를 정리한 뒤 게임오버 UI를 띄운다.
    /// </summary>
    public void GameOver()
    {
        isGameOver = true;

        // 현재 점수가 최고 점수보다 크면 저장.
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save();
        }

        // 최고 점수 UI 갱신
        bestScoreText.text = bestScore.ToString();

        // 남아 있는 장애물과 플레이어 Destroy.
        DestroyAllObstacles();
        DestroyPlayer();

        // 게임오버 UI 띄우기.
        gameOverImage.SetActive(true);
        restartButton.SetActive(true);

        // 전체 게임 시간을 멈춘다.
        Time.timeScale = 0f;
    }

    /// <summary>
    /// 현재 씬을 다시 불러와 게임을 재시작.
    /// </summary>
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// 태그가 Obstacle인 장애물 오브젝트를 삭제한다.
    /// </summary>
    private void DestroyAllObstacles()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

        foreach (GameObject obstacle in obstacles)
        {
            Destroy(obstacle);
        }
    }

    /// <summary>
    /// 태그가 Player인 플레이어 오브젝트를 삭제한다.
    /// </summary>
    private void DestroyPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Destroy(player);
        }
    }

    /// <summary>
    /// 현재 목숨 수에 맞게 하트 UI를 끈다.
    /// </summary>
    private void UpdateHeartUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].gameObject.SetActive(i < life);
        }
    }
}