using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Move Settings")]
    [SerializeField] private float speed = 4f;                // 장애물 이동 속도
    [SerializeField] private float rotationSpeed = 100f;      // 장애물 회전 속도

    [Header("Score Settings")]
    [SerializeField] private bool giveScore = false;          // 이 장애물이 점수를 주는 장애물인지 체크
    [SerializeField] private int scoreValue = 20;             // 화면 밖으로 사라졌을 때 추가할 점수

    [Header("Destroy Range")]
    [SerializeField] private float destroyMinX = -12f;        // 삭제할 X축 최소 범위
    [SerializeField] private float destroyMaxX = 10f;         // 삭제할 X축 최대 범위
    [SerializeField] private float destroyMinY = -7f;         // 삭제할 Y축 최소 범위
    [SerializeField] private float destroyMaxY = 6f;          // 삭제할 Y축 최대 범위

    private Vector2 moveDirection;                            // 장애물이 이동방향

    /// <summary>
    /// 장애물이 이동할 방향을 설정한다.
    /// </summary>
    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }

    /// <summary>
    /// SpawnManager가 난이도에 따라 현재 장애물 속도를 넣어준다.
    /// </summary>
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    private void Update()
    {
        // 설정된 방향으로 장애물을 이동.
        transform.position += (Vector3)(moveDirection * speed * Time.deltaTime);

        // 소행성을 회전시킴.
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);

        // 화면 밖으로 벗어나면 삭제한다.
        if (transform.position.x < destroyMinX || transform.position.x > destroyMaxX ||
            transform.position.y < destroyMinY || transform.position.y > destroyMaxY)
        {
            // 게임오버가 아니고, 점수를 주는 장애물이라면 점수를 추가한다.
            if (!GameManager.Instance.IsGameOver && giveScore)
            {
                GameManager.Instance.AddScore(scoreValue);
            }

            Destroy(gameObject);
        }
    }
}