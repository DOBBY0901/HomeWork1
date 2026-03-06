using UnityEngine;

public class SpawnManager : MonoBehaviour
{
   [SerializeField] private GameObject[] obstaclePrefabs;
   [SerializeField] private float spawnTime = 3f; //스폰 시간
   [SerializeField] private float spawnX = 10f;
   [SerializeField] private float spawnY = 6f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnTime)
        {
            SpawnObstacle();
            timer = 0f;
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
}