using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] public float speed; //장애물 속도
    [SerializeField] private float rotationSpeed = 100f; //장애물 회전 속도
    [SerializeField] private bool giveScore = false; //점수를 주는 장애물 체크
    [SerializeField] private int scoreValue = 20; //20점



    private Vector2 dir;

    public void SetDirection(Vector2 direction)
    {
        dir = direction.normalized;
    }

    void Update()
    {
        transform.position += (Vector3)(dir * speed * Time.deltaTime);
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        if (transform.position.x < -12f || transform.position.x > 10f ||
            transform.position.y < -7f || transform.position.y > 6f)
        {
            if (!GameManager.gameManager.isGameOver && giveScore)
            {
                GameManager.gameManager.AddScore(scoreValue);
            }

            Destroy(gameObject);
        }
    }
}