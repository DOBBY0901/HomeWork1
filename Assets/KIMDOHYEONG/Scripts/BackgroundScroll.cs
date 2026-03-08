using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [Header("Scroll Settings")]
    [SerializeField] private float speed = 1f;         // 배경이 아래로 움직이는 속도
    [SerializeField] private float height = 10f;       // 배경 한 장의 세로 길이

    private void Update()
    {
        // 배경을 아래 방향으로 이동.
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        // 배경이 아래쪽 끝까지 내려가면 위쪽으로 다시 올려 반복.
        if (transform.position.y <= -height)
        {
            transform.position += new Vector3(0f, height * 2f, 0f);
        }
    }
}