using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Move Settings")]
    [SerializeField] private float moveSpeed = 5f;           // 플레이어 이동 속도
    [SerializeField] private float limitX = 8.3f;            // 플레이어가 이동할 수 있는 X축 최대 범위
    [SerializeField] private float limitY = 4.2f;            // 플레이어가 이동할 수 있는 Y축 최대 범위

    private Rigidbody2D rb;                                  // 플레이어의 Rigidbody2D
    private Vector2 input;                                   // 플레이어 입력값 저장용 벡터

    private void Awake()
    {
        // 시작 전에 Rigidbody2D를 가져온다.
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // 게임오버 상태라면 입력X.
        if (GameManager.Instance.IsGameOver)
        {
            input = Vector2.zero;
            return;
        }

        // 좌우
        float horizontal = Input.GetAxisRaw("Horizontal");

        // 상하
        float vertical = Input.GetAxisRaw("Vertical");

        // normalized를 사용해서 대각선 이동 속도가 더 빨라지지 않도록 한다.
        input = new Vector2(horizontal, vertical).normalized;
    }

    private void FixedUpdate()
    {
        // Rigidbody2D를 이용해 실제 이동을 처리한다.
        rb.velocity = input * moveSpeed;

        // 현재 위치를 가져온다.
        Vector3 pos = transform.position;

        // 화면 밖으로 나가지 않도록 X, Y 좌표를 제한.
        pos.x = Mathf.Clamp(pos.x, -limitX, limitX);
        pos.y = Mathf.Clamp(pos.y, -limitY, limitY);

        // 좌표 제한 적용.
        transform.position = pos;
    }
}