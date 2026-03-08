using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour
{
    [Header("Respawn Settings")]
    [SerializeField] private Vector3 respawnPosition = Vector3.zero;   // 플레이어 리스폰 위치
    [SerializeField] private float invincibleTime = 1.5f;              // 리스폰 후 무적 시간
    [SerializeField] private float blinkInterval = 0.1f;               // 리스폰 후 깜빡깜빡

    private bool isInvincible = false;                                 // 현재 무적 상태인지 확인
    private SpriteRenderer spriteRenderer;                             // 플레이어 깜빡임 처리

    private void Awake()
    {
        // 시작 전에 SpriteRenderer를 가져온다.
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 장애물과 충돌했고, 현재 무적 상태가 아니라면 피격 처리
        if (collision.gameObject.CompareTag("Obstacle") && !isInvincible)
        {
            GameManager.Instance.PlayerHit();

            // 게임오버가 아니라면 리스폰 + 무적 처리
            if (!GameManager.Instance.IsGameOver)
            {
                transform.position = respawnPosition;
                StartCoroutine(InvincibleCoroutine());
            }
        }
    }

    private IEnumerator InvincibleCoroutine()
    {
        // 무적 시작
        isInvincible = true;

        float timer = 0f;

        // 무적 시간 동안 플레이어를 깜빡이게 해서 무적 상태임을 보여주기.
        while (timer < invincibleTime)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
        }

        // 무적 종료 시 깜빡임X.
        spriteRenderer.enabled = true;
        isInvincible = false;
    }
}