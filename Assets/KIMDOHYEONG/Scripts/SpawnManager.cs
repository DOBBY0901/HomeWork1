using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Obstacle Prefabs")]
    [SerializeField] private GameObject[] obstaclePrefabs;        // 랜덤으로 생성할 장애물 배열

    [Header("Spawn Settings")]
    [SerializeField] private float spawnTime = 3f;                // 장애물 생성 간격
    [SerializeField] private float minSpawnTime = 0.1f;           // 생성 간격이 줄어들 수 있는 최소값
    [SerializeField] private float spawnX = 10f;                  // 화면 바깥 X축 생성
    [SerializeField] private float spawnY = 6f;                   // 화면 바깥 Y축 생성

    [Header("Target Range")]
    [SerializeField] private float targetRangeX = 7f;             // 장애물이 향할 화면 안쪽 X 범위
    [SerializeField] private float targetRangeY = 4f;             // 장애물이 향할 화면 안쪽 Y 범위

    [Header("Obstacle Speed")]
    [SerializeField] private float obstacleSpeed = 4f;            // 현재 생성되는 장애물 기본 속도
    [SerializeField] private float maxObstacleSpeed = 10f;        // 장애물 최대 속도

    [Header("Difficulty Settings")]
    [SerializeField] private float difficultyTime = 5f;           // 몇 초마다 난이도를 올릴지
    [SerializeField] private float spawnTimeDecrease = 0.1f;      // 난이도 증가 시 생성 간격 감소량
    [SerializeField] private float speedIncrease = 0.5f;          // 난이도 증가 시 장애물 속도 증가량

    private float timer;                                          // 스폰 타이머
    private float difficultyTimer;                                // 난이도 증가 타이머

    private void Update()
    {
        // 게임오버 상태면 더 이상 생성하거나 난이도를 올리지 않는다.
        if (GameManager.Instance.IsGameOver) return;

        timer += Time.deltaTime;
        difficultyTimer += Time.deltaTime;

        // 현재 생성 간격에 스폰 타이머가 도달하면 장애물을 생성한다.
        if (timer >= spawnTime)
        {
            SpawnObstacle();
            timer = 0f;
        }

        // 일정 시간이 지나면 난이도를 올린다.
        if (difficultyTimer >= difficultyTime)
        {
            IncreaseDifficulty();
            difficultyTimer = 0f;
        }
    }

    /// <summary>
    /// 화면 바깥 랜덤한 곳에서 장애물을 생성하고,화면 안의 랜덤 위치를 향하도록 설정한다.
    /// </summary>
    private void SpawnObstacle()
    {
        Vector2 spawnPosition = Vector2.zero;

        // 0: 위, 1: 아래, 2: 왼쪽, 3: 오른쪽
        int side = Random.Range(0, 4);

        if (side == 0)
        {
            spawnPosition = new Vector2(Random.Range(-spawnX, spawnX), spawnY);
        }
        else if (side == 1)
        {
            spawnPosition = new Vector2(Random.Range(-spawnX, spawnX), -spawnY);
        }
        else if (side == 2)
        {
            spawnPosition = new Vector2(-spawnX, Random.Range(-spawnY, spawnY));
        }
        else
        {
            spawnPosition = new Vector2(spawnX, Random.Range(-spawnY, spawnY));
        }

        // 장애물이 향할 화면 안쪽의 랜덤 목표 지점
        Vector2 targetPosition = new Vector2(Random.Range(-targetRangeX, targetRangeX),Random.Range(-targetRangeY, targetRangeY));
        // 생성 위치에서 목표 위치로 향하는 방향 벡터
        Vector2 moveDirection = targetPosition - spawnPosition;

        // 배열 중 하나를 랜덤으로 선택해서 생성
        int randomIndex = Random.Range(0, obstaclePrefabs.Length);
        GameObject obstacleObject = Instantiate(obstaclePrefabs[randomIndex], spawnPosition, Quaternion.identity);

        // 생성된 장애물에게 방향과 현재 속도 부여
        Obstacle obstacle = obstacleObject.GetComponent<Obstacle>();
        obstacle.SetDirection(moveDirection);
        obstacle.SetSpeed(obstacleSpeed);
    }

    /// <summary>
    /// 시간이 지남에 따라 생성 간격은 줄이고, 장애물 속도는 증가시킨다.
    /// 단, 각각 최소/최대 제한을 넘지 않도록 한다.
    /// </summary>
    private void IncreaseDifficulty()
    {
        if (spawnTime > minSpawnTime)
        {
            spawnTime -= spawnTimeDecrease;

            if (spawnTime < minSpawnTime)
            {
                spawnTime = minSpawnTime;
            }
        }

        if (obstacleSpeed < maxObstacleSpeed)
        {
            obstacleSpeed += speedIncrease;

            if (obstacleSpeed > maxObstacleSpeed)
            {
                obstacleSpeed = maxObstacleSpeed;
            }
        }
    }
}