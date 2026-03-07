using UnityEngine;

public class SpawnManager : MonoBehaviour
{
   [SerializeField] private GameObject[] obstaclePrefabs;
   [SerializeField] private float spawnTime = 3f; //스폰 시간
   [SerializeField] private float minSpawnTime = 0.1f; //최소 스폰 시간

   [SerializeField] private float spawnX = 10f; //스폰x좌표
   [SerializeField] private float spawnY = 6f; //스폰 y좌표

   [SerializeField] private float obstacleSpeed = 4f; //생성되는 장애물 기본 속도
   [SerializeField] private float maxObstacleSpeed = 10f; //장애물 최고 속도

   [SerializeField] private float difficultyTime = 5f; //난이도가 증가하는 초
   [SerializeField] private float spawnTimeDecrease = 0.1f; //스폰이 어느정도로 빨라질건지
   [SerializeField] private float speedIncrease = 0.5f; //속도가 얼마나 빨라질건지

    private float timer;
    private float difficultyTimer;

    void Update()
    {
        timer += Time.deltaTime;
        difficultyTimer += Time.deltaTime;

        if (timer >= spawnTime)
        {
            SpawnObstacle();
            timer = 0f;
        }
        if (difficultyTimer >= difficultyTime)
        {
            IncreaseDifficulty();
            difficultyTimer = 0f;
        }

    }

    void SpawnObstacle()
    {
        Vector2 spawnPos = Vector2.zero;

        int side = Random.Range(0, 4);

        if (side == 0)
        {
            spawnPos = new Vector2(Random.Range(-spawnX, spawnX), spawnY);
        }
        else if (side == 1)
        {
            spawnPos = new Vector2(Random.Range(-spawnX, spawnX), -spawnY);
        }
        else if (side == 2)
        {
            spawnPos = new Vector2(-spawnX, Random.Range(-spawnY, spawnY));
        }
        else
        {
            spawnPos = new Vector2(spawnX, Random.Range(-spawnY, spawnY));
        }

        Vector2 targetPos = new Vector2(
            Random.Range(-7f, 7f),
            Random.Range(-4f, 4f)
        );

        Vector2 moveDir = targetPos - spawnPos;

        int randomIndex = Random.Range(0, obstaclePrefabs.Length);
        GameObject obj = Instantiate(obstaclePrefabs[randomIndex], spawnPos, Quaternion.identity);

        obj.GetComponent<Obstacle>().SetDirection(moveDir);
    }

    void IncreaseDifficulty()
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