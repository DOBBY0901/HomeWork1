using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverImage; //게임오버 이미지
    [SerializeField] private GameObject restartButton; //재시작 버튼

    public static GameManager gameManager;
    public bool isGameOver = false;

    public int score = 0; //점수
    private float scoreTimer = 0f; //시간
    public float survivalTime = 0f; //생존시간

    void Awake()
    {
        gameManager = this;
    }

    void Start()
    {
        gameOverImage.SetActive(false);
        restartButton.SetActive(false);
        Time.timeScale = 1f;
    }
    private void Update()
    {
        if(isGameOver) return;

        scoreTimer += Time.deltaTime;
        survivalTime += Time.deltaTime;

        if (scoreTimer >= 1f)
        {
            score += 10; //초당 10점씩 추가
            scoreTimer = 0f;
        }
    }

    //점수 추가
    public void AddScore(int value)
    {
        score += value;
    }

    //게임 종료
    public void GameOver()
    {
        isGameOver = true;

        DestroyAllObstacles();
        DestroyPlayer();

        gameOverImage.SetActive(true);
        restartButton.SetActive(true);

        Time.timeScale = 0f;
    }

    //재시작
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //장애물 삭제
    void DestroyAllObstacles()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

        foreach (GameObject obj in obstacles)
        {
            Destroy(obj);
        }
    }

    //플레이어 삭제
    void DestroyPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Destroy(player);
        }
    }
}