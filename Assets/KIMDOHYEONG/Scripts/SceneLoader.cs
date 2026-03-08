using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private AudioSource clickSound;       // 시작 버튼 클릭 효과음 재생용 AudioSource
    [SerializeField] private float loadDelay = 3f;       // 클릭 후 씬 이동 전 대기 시간 - 효과음 

    [Header("Scene Settings")]
    [SerializeField] private string gameSceneName = "Game"; // 이동할 게임 씬

    /// <summary>
    /// 시작 버튼에서 호출되는 함수.
    /// 코루틴을 실행해서 효과음 재생 후 씬 이동을 처리한다.
    /// </summary>
    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }

    private IEnumerator StartGameCoroutine()
    {
        // 버튼 클릭 효과음 재생
        if (clickSound != null)
        {
            clickSound.Play();
        }

        // 대기
        yield return new WaitForSeconds(loadDelay);

        // 지정된 게임 씬으로 이동
        SceneManager.LoadScene(gameSceneName);
    }
}