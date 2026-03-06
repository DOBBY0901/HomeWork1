using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5; //플레이어 속도

    private Rigidbody2D rb;
    private Vector2 input;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal"); //상하
        float v = Input.GetAxisRaw("Vertical"); //좌우

        input = new Vector2(h, v).normalized;  //대각선 이동이 빨라지는것을 막기
    }

    void FixedUpdate()
    {
        rb.velocity = input * moveSpeed;

        Vector3 pos = transform.position;  //캐릭터 카메라 밖으로 나가지 않게

        pos.x = Mathf.Clamp(pos.x, -8.3f, 8.3f); // x범위
        pos.y = Mathf.Clamp(pos.y, -4.2f, 4.2f); //y범위

        transform.position = pos;
    }
}